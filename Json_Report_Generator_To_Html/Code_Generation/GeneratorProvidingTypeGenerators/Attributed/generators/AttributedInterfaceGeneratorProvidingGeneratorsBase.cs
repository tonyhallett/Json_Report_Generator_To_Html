using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Attributes;
using Reinforced.Typings.Generators;
using System;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public abstract class AttributedInterfaceGeneratorProvidingGeneratorsBase : InterfaceGeneratorProvidingGeneratorsBase
    {
        private IGeneratedMemberHandler generatedMemberHandler;
        private bool lazy;
        public AttributedInterfaceGeneratorProvidingGeneratorsBase(IGeneratedMemberHandler generatedMemberHandler = null, bool lazy = true)
        {
            this.generatedMemberHandler = generatedMemberHandler;
            this.lazy = lazy;

        }
        protected override IGeneratorProvidingTypeGeneratorForMembers GeneratorProvidingTypeGenerator => new AttributedGeneratorProvidingTypeGeneratorsForMembers(generatedMemberHandler, lazy);

    }

}
