namespace BrowserControl
{
    public interface IScriptManager
    {
        void BuyMeACoffee();
        void Initialize(IWindowExternalCallbackWriter callbackWriter, ISettings settings, IReportGenerator reportGenerator);
        void Log(string message);
        void LogError(string message);
        void LogIssueOrSuggestion();
        void OpenFile(string assemblyName, string qualifiedClassName);
        void RateAndReview();

        void GenerateReport();
    }
}