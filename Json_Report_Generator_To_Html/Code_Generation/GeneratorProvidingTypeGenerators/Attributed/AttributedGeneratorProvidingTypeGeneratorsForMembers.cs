using Reinforced.Typings;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public class AttributedGeneratorProvidingTypeGeneratorsForMembers : GeneratorProvidingTypeGeneratorForMembers
    {
        private readonly bool lazy;

        public AttributedGeneratorProvidingTypeGeneratorsForMembers(IGeneratedMemberHandler generatedMemberHandler = null,bool lazy = true):base(generatedMemberHandler)
        {
            this.lazy = lazy;
        }
        protected override GeneratorProvidingTypeGeneratorsBase GetActual(ExportContext context)
        {
            return new AttributedGeneratorProvidingTypeGenerators(context,true,lazy);
        }
    }
}
