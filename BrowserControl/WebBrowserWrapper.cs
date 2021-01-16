using BrowserControl.Deserialization;
using Newtonsoft.Json;
using System.Linq;
using System.Windows.Controls;

namespace BrowserControl
{
    class ProjectsAdded
    {
        public TestProject[] projects { get; set; }
        public bool newSolution { get; set; }
        public ProjectsAdded(TestProject[] newProjects, bool newSolution)
        {
            this.newSolution = newSolution;
            this.projects = newProjects;
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class WebBrowserWrapper:IWebBrowser,IJsReportProxy
    {
        private readonly WebBrowser webBrowser;

        public WebBrowserWrapper(WebBrowser webBrowser)
        {
            this.webBrowser = webBrowser;
        }

        #region IJsReportProxy
        public void ChangeShowExpandCollapseAll(bool expandCollapseAll)
        {
            webBrowser.InvokeScript("changeShowExpandCollapseAll", JsonConvert.SerializeObject(expandCollapseAll));
        }

        public void ChangeShowFilter(bool showFilter)
        {
            webBrowser.InvokeScript("changeShowFilter", JsonConvert.SerializeObject(showFilter));
        }

        public void ChangeShowTooltips(bool showTooltips)
        {
            webBrowser.InvokeScript("changeShowTooltips", JsonConvert.SerializeObject(showTooltips));
        }

        public void ReportGenerationEnabled(bool enabled)
        {
            webBrowser.InvokeScript("reportGenerationEnabled", JsonConvert.SerializeObject(enabled));
        }

        public void GenerateReport(string jsonSummaryResultJson)
        {
            webBrowser.InvokeScript("generateReport", jsonSummaryResultJson);
        }

        public void Initialize(string settingsJson)
        {
            webBrowser.InvokeScript("initialize", settingsJson);
        }

        public void RunningReport()
        {
            webBrowser.InvokeScript("runningReport");
        }
        public void ProjectsAdded(TestProject[] newProjects, bool newSolution)
        {
            webBrowser.InvokeScript("projectsAdded", new ProjectsAdded(newProjects, newSolution).ToJson());
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
