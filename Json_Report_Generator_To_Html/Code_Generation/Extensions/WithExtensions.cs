using Reinforced.Typings.Fluent;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public static class WithExtensions
    {
        public static ClassOrInterfaceExportBuilder WithAllMembers(this ClassOrInterfaceExportBuilder classOrInterfaceExportBuilder)
        {
            return classOrInterfaceExportBuilder.WithAllProperties().WithAllMethods().WithAllFields();
        }
    }
}
