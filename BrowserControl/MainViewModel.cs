using BrowserControl.Deserialization;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToastNotifications.Messages;

namespace BrowserControl
{

    public class MainViewModel : ViewModelBase, IWindowExternalCallbackWriter, IReportGenerator, IInitializeSettings
    {
        private bool initialized;
        private readonly IWebBrowser webBrowser;
        private readonly IGenerator generator;
        private readonly IToaster toaster;
        private readonly IJsReportProxy jsReportProxy;
        public RelayCommand<object> SelectProjectCommand { get; set; }
        public RelayCommand<object> ClearProjectsCommand { get; set; }
        
        public SettingsViewModel Settings { get; set; }
        private bool useFakeData = true;
        private bool useFakeDataEnabled;
        public bool UseFakeData
        {
            get => useFakeData;
            set => SetProperty(ref useFakeData, value);
        }
        public bool UseFakeDataEnabled
        {
            get => useFakeDataEnabled;
            set => SetProperty(ref useFakeDataEnabled, value);
        }
        private void AddTestProject(TestProject testProject)
        {
            this.TestProjects.Add(testProject);
            UseFakeDataEnabled = true;
            UseFakeData = false;
        }
        public ObservableCollection<TestProject> TestProjects { get; } = new ObservableCollection<TestProject>();
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
            scriptManager.Initialize(this,Settings, this);
            webBrowser.ObjectForScripting = scriptManager;
            webBrowser.Navigate(htmlProvider.GetPath());
            this.webBrowser = webBrowser;
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
                    this.AddTestProject(new TestProject { Name = name, DllPath = dll });
                }
            });
            ClearProjectsCommand = new RelayCommand<object>(_ =>
            {
                TestProjects.Clear();
                UseFakeDataEnabled = false;
                UseFakeData = true;
            });
        }


        public void Received(string message)
        {
            toaster.ShowInformation(message);
        }

        public void GenerateReportFromFakeData()
        {
            webBrowser.RunningReport();
            Task.Delay(generator.FakeGenerationTime).ContinueWith((_) =>
            {
                webBrowser.GenerateReport(generator.GenerateFakeData());
            });
            
            
        }

        public void GenerateReport()
        {
            webBrowser.RunningReport();
            // let the generator deal with ensuring output folder
            generator.Generate(TestProjects.Select(p => p.DllPath).ToList());//todo async

        }

        public void Generate()
        {
            if (!this.initialized)
            {
                toaster.ShowWarning("Initialize settings first");
            }
            else
            {
                if (this.TestProjects.Count == 0 || UseFakeData)
                {
                    this.GenerateReportFromFakeData();
                }
                else
                {
                    GenerateReport();
                }
            }
            
        }

        public void Initialize(ISettings settings)
        {
            initialized = true;
            jsReportProxy.Initialize(settings);
        }
    }
}
