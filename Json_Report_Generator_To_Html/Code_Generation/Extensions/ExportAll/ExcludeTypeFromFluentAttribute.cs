using System;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface|AttributeTargets.Struct, AllowMultiple = false)]
    public class ExcludeTypeFromFluentAttribute : Attribute { }
}
