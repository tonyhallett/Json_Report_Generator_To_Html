using Newtonsoft.Json;

namespace BrowserControl
{
    public class JsonSettings
    {
        public bool reportGenerationEnabled { get; set; }
        public bool showExpandCollapseAll { get; set; } = true;
        public bool showFilter { get; set; } = true;
        public bool showTooltips { get; set; } = true;
        public bool showGroupSlider { get; set; } = true;
    }
    public class SettingsViewModel: ViewModelBase, ISettings
    {
        public SettingsViewModel(IJsReportProxy jsReportProxy,IInitializeSettings initializeSettings)
        {
            this.jsReportProxy = jsReportProxy;
            InitializeCommand = new RelayCommand<object>(_ =>
            {
                InitializeEnabled = false;
                var json = JsonConvert.SerializeObject(this);
                initializeSettings.Initialize(new JsonSettings { reportGenerationEnabled=this.ReportGenerationEnabled});
            });
        }
        private bool reportGenerationEnabled = true;
        private readonly IJsReportProxy jsReportProxy;
        private bool initializeEnabled = true;
        public bool InitializeEnabled
        {
            get => initializeEnabled;
            set => SetProperty(ref initializeEnabled, value);
        }
        public RelayCommand<object> InitializeCommand { get; set; }
        public bool ReportGenerationEnabled
        {
            get => reportGenerationEnabled;
            set
            {
                SetProperty(ref reportGenerationEnabled, value);
                jsReportProxy.ReportGenerationEnabled(value);
            }
        }

    }

    
}
