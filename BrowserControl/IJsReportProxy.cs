using BrowserControl.Deserialization;

namespace BrowserControl
{
    public interface IJsReportProxy
    {
        void ReportGenerationEnabled(bool enabled);
        void GenerateReport(JsonSummaryResult jsonSummaryResult);
        void Initialize(ProxySettings initialSettings);
        void RunningReport();
        
    }
}
