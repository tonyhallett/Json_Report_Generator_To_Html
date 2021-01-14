using System;
using System.Collections.Generic;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation.GeneratorProvidingTypeGenerators.Contained
{
    public class DemoContainedInterfaceGeneratorProvidingGenerators: ContainedInterfaceGeneratorProvidingGenerators
    {
        public DemoContainedInterfaceGeneratorProvidingGenerators() : base(new Dictionary<Type, Type>
        {
            { typeof(PropertyInfo), typeof(DemoPropertyCodeGenerator)}
        })
        {

        }
    }
}
