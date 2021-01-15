using BrowserControl.Deserialization;

namespace BrowserControl
{
    public interface IWebBrowser
    {
        IScriptManager ObjectForScripting { set; }

        void Navigate(string path);
        
        void RunningReport();
        void GenerateReport(JsonSummaryResult jsonSummaryResult);

        void ReportGenerationEnabled(bool enabled);
    }

    
}
