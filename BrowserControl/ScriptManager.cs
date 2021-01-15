using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BrowserControl
{
    [ComVisible(true)]
    public class ScriptManager : IScriptManager
    {
        private IReportGenerator reportGenerator;
        private IWindowExternalCallbackWriter windowExternalCallbackWriter;
        private ISettings settings;

        public void Initialize(IWindowExternalCallbackWriter callbackWriter, ISettings settings,IReportGenerator reportGenerator)
        {
            this.reportGenerator = reportGenerator;
            this.windowExternalCallbackWriter = callbackWriter;
            this.settings = settings;
        }

        public void ReportGenerationEnabled(bool enabled)
        {
            settings.ReportGenerationEnabled = enabled;
        }
        public void GenerateReport()
        {
            reportGenerator.Generate();
        }
        #region calls from js that just write
        //commented out in js currently
        public void LogError(string message)
        {
            this.windowExternalCallbackWriter.Received($"Error - {message}");
        }
        #region link backs that write
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
        public void OpenFile(string assemblyName, string qualifiedClassName)
        {
            this.windowExternalCallbackWriter.Received($"{assemblyName} {qualifiedClassName}");
        }

        public void BuyMeACoffee()
        {
            //System.Diagnostics.Process.Start("https://paypal.me/FortuneNgwenya");
            this.windowExternalCallbackWriter.Received("Buy me a coffee");
        }

        public void LogIssueOrSuggestion()
        {
            //System.Diagnostics.Process.Start("https://github.com/FortuneN/FineCodeCoverage/issues");
            this.windowExternalCallbackWriter.Received("github issues");
        }

        public void RateAndReview()
        {
            this.windowExternalCallbackWriter.Received("rate");
            //System.Diagnostics.Process.Start("https://marketplace.visualstudio.com/items?itemName=FortuneNgwenya.FineCodeCoverage&ssr=false#review-details");
        }
        #endregion
        #endregion
        
    }

}
