using Reinforced.Typings;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public static class LogExtensions
    {
        public static void Log(this ExportContext context, string message)
        {
            context.Warnings.Add(new Reinforced.Typings.Exceptions.RtWarning(0, "Log", message));
        }
    }
}
