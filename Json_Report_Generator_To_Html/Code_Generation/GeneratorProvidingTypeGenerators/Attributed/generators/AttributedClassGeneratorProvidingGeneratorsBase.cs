namespace Json_Report_Generator_To_Html.Code_Generation
{
    public abstract class AttributedClassGeneratorProvidingGeneratorsBase: ClassGeneratorProvidingGeneratorsBase
    {
        private IGeneratedMemberHandler generatedMemberHandler;
        private bool lazy;
        public AttributedClassGeneratorProvidingGeneratorsBase(IGeneratedMemberHandler generatedMemberHandler = null, bool lazy = true)
        {
            this.generatedMemberHandler = generatedMemberHandler;
            this.lazy = lazy;

        }
        protected override IGeneratorProvidingTypeGeneratorForMembers GeneratorProvidingTypeGenerator => new AttributedGeneratorProvidingTypeGeneratorsForMembers(generatedMemberHandler, lazy);

    }

}
