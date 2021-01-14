using Reinforced.Typings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation.GeneratorProvidingTypeGenerators.Contained
{
    //relationship Type and members ....
    //migt want to do on a Type basis - public void GenerateMembers<T>(Type element, ... IEnumerable<T> members,
    public class ContainedGeneratorProvidingTypeGenerator : GeneratorProvidingTypeGeneratorsWhenDefault
    {
        private readonly Dictionary<Type, Type> codeGenerators;

        public ContainedGeneratorProvidingTypeGenerator(Dictionary<Type,Type> codeGenerators, ExportContext exportContext, bool provideGeneratorsForParameters = true, bool lazy = true) : base(exportContext, provideGeneratorsForParameters, lazy)
        {
            this.codeGenerators = codeGenerators;
        }

        
        private Type ForType(object @for)
        {
            var type = @for.GetType();
            foreach(var kvp in codeGenerators)
            {
                if (kvp.Key.IsAssignableFrom(type))
                {
                    return kvp.Value;
                }
            }
            
            return null;
        }
        protected override Type ProvideConstructorGeneratorTypeWhenDefault(ConstructorInfo constructor)
        {
            return ForType(constructor);
        }

        protected override Type ProvideFieldGeneratorTypeWhenDefault(FieldInfo field)
        {
            return ForType(field);
        }

        protected override Type ProvideMethodGeneratorTypeWhenDefault(MethodInfo method)
        {
            return ForType(method);
        }

        protected override Type ProvideParameterGeneratorTypeWhenDefault(ParameterInfo parameter)
        {
            return ForType(parameter);
        }

        protected override Type ProvidePropertyGeneratorTypeWhenDefault(PropertyInfo property)
        {
            return ForType(property);
        }
    }
}
