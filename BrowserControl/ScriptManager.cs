using Newtonsoft.Json;
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
        private IJsReportProxy jsReportProxy;

        public void Initialize(IWindowExternalCallbackWriter callbackWriter, ISettings settings,IReportGenerator reportGenerator,IJsReportProxy jsReportProxy)
        {
            this.reportGenerator = reportGenerator;
            this.windowExternalCallbackWriter = callbackWriter;
            this.settings = settings;
            this.jsReportProxy = jsReportProxy;
        }

        #region mimic vs
        public void ChangeShowExpandCollapseAll(bool expandCollapseAll)
        {
            jsReportProxy.ChangeShowExpandCollapseAll(expandCollapseAll);
        }

        public void ChangeShowFilter(bool showFilter)
        {
            jsReportProxy.ChangeShowFilter(showFilter);
        }

        public void ChangeShowTooltips(bool showTooltips)
        {
            jsReportProxy.ChangeShowTooltips(showTooltips);
        }
        #endregion

        public void ReportGenerationEnabled(bool enabled)
        {
            settings.ReportGenerationEnabled = enabled;
        }
        public void GenerateReport(string projectsJson)
        {
            var projects = JsonConvert.DeserializeObject<TestProject[]>(projectsJson);
            reportGenerator.Generate(projects);
        }
        #region calls from js that just write
        //commented out in js currently
        public void LogError(string message)
        {
            this.windowExternalCallbackWriter.ReceivedError(message);
        }
        #region link backs that write
        public void Log(string message)
        {
            this.windowExternalCallbackWriter.Received(message);
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
