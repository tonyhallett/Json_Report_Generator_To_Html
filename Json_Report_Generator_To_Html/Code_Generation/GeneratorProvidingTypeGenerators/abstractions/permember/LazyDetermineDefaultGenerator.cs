using Reinforced.Typings;
using Reinforced.Typings.Attributes;
using System;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    #region IDetermineDefaultGenerator
    internal class LazyDetermineDefaultGenerator : IDetermineDefaultGenerator
    {
        private readonly ExportContext exportContext;

        public LazyDetermineDefaultGenerator(ExportContext exportContext)
        {
            this.exportContext = exportContext;
        }
        public bool MemberHasDefaultGenerator<TMember>(TMember member) where TMember : MemberInfo
        {
            return GetGeneratorTypeFromAttributeOrFluent(member) == null;
        }

        private Type GetGeneratorTypeFromAttributeOrFluent<T>(T member) where T : MemberInfo
        {
            Type GetFromAttribute(TsAttributeBase attr)
            {
                if (attr != null)
                {
                    return attr.CodeGeneratorType;
                }
                return null;
            }

            var attr = exportContext.CurrentBlueprint.ForMember<TsTypedMemberAttributeBase>(member);
            var fromAttr = GetFromAttribute(attr);
            if (fromAttr != null) return fromAttr;

            // internal knowledge
            if (member is MethodInfo)
            {
                var classAttr = exportContext.CurrentBlueprint.Attr<TsClassAttribute>();
                if (classAttr != null && classAttr.DefaultMethodCodeGenerator != null)
                {
                    return classAttr.DefaultMethodCodeGenerator;
                }
            }
            return null;

        }

        public bool ParameterHasDefaultGenerator(ParameterInfo parameter)
        {
            return exportContext.CurrentBlueprint.ForMember(parameter).CodeGeneratorType == null;
        }
    }

#endregion
}
