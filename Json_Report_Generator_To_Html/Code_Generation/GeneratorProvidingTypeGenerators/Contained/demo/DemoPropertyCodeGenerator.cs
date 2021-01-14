using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Generators;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation.GeneratorProvidingTypeGenerators.Contained
{
    internal class DemoPropertyCodeGenerator : PropertyCodeGenerator
    {
        public override RtField GenerateNode(MemberInfo element, RtField result, TypeResolver resolver)
        {
            Context.Warnings.Add(new Reinforced.Typings.Exceptions.RtWarning(0, "Log", "Hello!"));
            return base.GenerateNode(element, result, resolver);
        }
    }
}
