using BrowserControl.Deserialization;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToastNotifications.Messages;

namespace BrowserControl
{
    public static class FakeNames
    {
        public static string Fake1 => "___Fake1";
        public static string Fake2 => "___Fake2";
    }

    public class MainViewModel : ViewModelBase, IWindowExternalCallbackWriter, IReportGenerator, IInitializeSettings
    {
        private bool initialized;
        private readonly IGenerator generator;
        private readonly IToaster toaster;
        private readonly IJsReportProxy jsReportProxy;
        public RelayCommand<object> SelectProjectCommand { get; set; }
        public RelayCommand<object> ClearProjectsCommand { get; set; }
        
        public SettingsViewModel Settings { get; set; }
        private void AddTestProject(TestProject testProject)
        {
            this.TestProjects.Add(testProject);
            if (initialized)
            {
                jsReportProxy.ProjectsAdded(new TestProject[] { testProject }, false);
            }
        }
        public ObservableCollection<TestProject> TestProjects { get; } = new ObservableCollection<TestProject>
        {
            new TestProject{ name=FakeNames.Fake1,path="Some/FakePath/Fake1.dll", Fake = true},
            new TestProject{ name=FakeNames.Fake2,path="Some/FakePath/Fake2.dll", Fake = true}
        };

        public MainViewModel(
            IWebBrowser webBrowser,
            IJsReportProxy jsReportProxy,
            IHtmlProvider htmlProvider, 
            IScriptManager scriptManager, 
            IGenerator generator,
            IToaster toaster
        )
        {
            Settings = new SettingsViewModel(jsReportProxy, this);
            scriptManager.Initialize(this,Settings, this,jsReportProxy);
            webBrowser.ObjectForScripting = scriptManager;
            webBrowser.Navigate(htmlProvider.GetPath());
            this.generator = generator;
            this.toaster = toaster;
            this.jsReportProxy = jsReportProxy;
            SelectProjectCommand = new RelayCommand<object>(_ =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "dll|*.dll";
                if (openFileDialog.ShowDialog() == true)
                {
                    var dll = openFileDialog.FileName;
                    var name = System.IO.Path.GetFileNameWithoutExtension(dll);
                    this.AddTestProject(new TestProject { name = name, path = dll });
                }
            });
            ClearProjectsCommand = new RelayCommand<object>(_ =>
            {
                TestProjects.Clear();
                //going to mimic a new solution here
                jsReportProxy.ProjectsAdded(new TestProject[] { }, true);
            });
        }


        public void Received(string message)
        {
            toaster.ShowInformation(message);
        }

        public void ReceivedError(string message)
        {
            toaster.ShowError(message);
        }

        public void GenerateReportFromFakeData(string[] testProjectNames)
        {
            jsReportProxy.RunningReport();

            Task.Delay(generator.FakeGenerationTime).ContinueWith((_) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                 {
                     jsReportProxy.GenerateReport(generator.GenerateFakeData(testProjectNames));
                 });
            });

        }

        public void GenerateReport(TestProject[] testProjects)
        {
            jsReportProxy.RunningReport();
            Task.Run(() =>
            {
                var generatedReportJson = generator.Generate(testProjects.Select(p => p.path).ToList());
                App.Current.Dispatcher.Invoke(() =>
                {
                    jsReportProxy.GenerateReport(generatedReportJson);
                });
            });
        }

        public void Generate(TestProject[] testProjects)
        {
            if (!initialized)
            {
                toaster.ShowWarning("Initialize settings first");
            }
            else
            {
                var fakeProjects = testProjects.Where(p => p.Fake).ToArray();
                var numberOfFakes = fakeProjects.Length;
                if (numberOfFakes == testProjects.Length)
                {
                    GenerateReportFromFakeData(fakeProjects.Select(p=>p.name).ToArray());
                }
                else
                {
                    if(numberOfFakes == 0)
                    {
                        toaster.ShowInformation("Excluded fake projects");
                    }
                    GenerateReport(testProjects.Except(fakeProjects).ToArray());
                }
            }
            
        }
        
        public void Initialize(JsonSettings jsonSettings)
        {
            initialized = true;
            jsReportProxy.Initialize(new InitializationSettings(jsonSettings,TestProjects.ToArray()).ToJson());
        }
    }

    class InitializationSettings : JsonSettings
    {
        public InitializationSettings(JsonSettings jsonSettings,TestProject[] projects)
        {
            reportGenerationEnabled = jsonSettings.reportGenerationEnabled;
            showExpandCollapseAll = jsonSettings.showExpandCollapseAll;
            showFilter = jsonSettings.showFilter;
            showGroupSlider = jsonSettings.showGroupSlider;
            showTooltips = jsonSettings.showTooltips;
            this.projects = projects;
        }
        public TestProject[] projects { get; }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
