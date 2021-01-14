using Reinforced.Typings.Fluent;
using System;
using System.Linq;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public static class ExportAllExtensions
    {
        public static void ExportAllInNamespaceAsInterfaces(this ConfigurationBuilder builder,Type typeWithNamespace,Action<InterfaceExportBuilder> configuration = null)
        {
            var assembly = typeWithNamespace.Assembly;
            var namespaceName = typeWithNamespace.Namespace;
            var typesInNamespace = assembly.GetTypes().Where(t => t.Namespace == namespaceName).Where(t => t.GetCustomAttribute<ExcludeTypeFromFluentAttribute>() == null);
            builder.ExportAsInterfaces(typesInNamespace, configuration);
        }
        
        
    }
}
