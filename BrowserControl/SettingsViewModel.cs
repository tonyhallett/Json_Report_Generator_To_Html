namespace BrowserControl
{

    public class SettingsViewModel: ViewModelBase, ISettings
    {
        public SettingsViewModel(IJsReportProxy jsReportProxy,IInitializeSettings initializeSettings)
        {
            this.jsReportProxy = jsReportProxy;
            InitializeCommand = new RelayCommand<object>(_ =>
            {
                InitializeEnabled = false;
                initializeSettings.Initialize(this);
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
