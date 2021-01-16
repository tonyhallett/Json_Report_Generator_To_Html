namespace BrowserControl
{
    public interface IReportGenerator
    {
        void Generate(TestProject[] testProjects);
    }
}