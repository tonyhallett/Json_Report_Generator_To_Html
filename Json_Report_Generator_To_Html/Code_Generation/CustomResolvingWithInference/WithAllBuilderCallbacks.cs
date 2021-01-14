using Reinforced.Typings.Fluent;
using System;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public class WithAllBuilderCallbacks
    {
        public Action<PropertyExportBuilder> PropertyCallback { get; set; }
        public Action<PropertyExportBuilder> FieldCallback { get; set; }
        public Action<MethodExportBuilder> MethodCallback { get; set; }
        
    }
}
