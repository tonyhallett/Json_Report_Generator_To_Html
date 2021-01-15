using BrowserControl.Deserialization;
using System.Linq;
using System.Windows.Controls;

namespace BrowserControl
{
    public class WebBrowserWrapper:IWebBrowser,IJsReportProxy
    {
        private readonly WebBrowser webBrowser;

        public WebBrowserWrapper(WebBrowser webBrowser)
        {
            this.webBrowser = webBrowser;
        }

        #region IJsReportProxy

        public void ReportGenerationEnabled(bool enabled)
        {
            webBrowser.InvokeScript("reportGenerationEnabled", enabled);
        }

        public void GenerateReport(JsonSummaryResult jsonSummaryResult)
        {
            webBrowser.InvokeScript("generateReport", jsonSummaryResult);
        }

        public void Initialize(ProxySettings settings)
        {
            webBrowser.InvokeScript("initialize", settings);
        }

        public void RunningReport()
        {
            webBrowser.InvokeScript("runningReport");
        }

        #endregion
        #region IWebBrowser
        public IScriptManager ObjectForScripting { set => webBrowser.ObjectForScripting = value; }
        
        public void Navigate(string path)
        {
            webBrowser.Navigate(path);
        }
        #endregion


    }

    
}
