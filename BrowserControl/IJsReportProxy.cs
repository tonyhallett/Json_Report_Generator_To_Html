using BrowserControl.Deserialization;

namespace BrowserControl
{
    public interface IJsReportProxy
    {
        void ReportGenerationEnabled(bool enabled);
        void GenerateReport(string jsonSummaryResultJson);
        void Initialize(string initialSettingsJson);
        void RunningReport();
        void ProjectsAdded(TestProject[] newProjects, bool newSolution);

        void ChangeShowExpandCollapseAll(bool expandCollapseAll);


        void ChangeShowFilter(bool showFilter);


        void ChangeShowTooltips(bool showTooltips);
        


    }
}
