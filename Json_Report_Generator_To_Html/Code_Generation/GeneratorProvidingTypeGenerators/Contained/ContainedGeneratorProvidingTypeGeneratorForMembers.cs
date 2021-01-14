﻿using Reinforced.Typings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Json_Report_Generator_To_Html.Code_Generation.GeneratorProvidingTypeGenerators.Contained
{
    public class ContainedGeneratorProvidingTypeGeneratorForMembers : GeneratorProvidingTypeGeneratorForMembers
    {
        private readonly Dictionary<Type, Type> codeGenerators;
        private readonly bool provideGeneratorsForParameters;
        private readonly bool lazy;

        public ContainedGeneratorProvidingTypeGeneratorForMembers(Dictionary<Type,Type> codeGenerators,IGeneratedMemberHandler generatedMemberHandler = null, bool provideGeneratorsForParameters = true, bool lazy = true):base(generatedMemberHandler)
        {
            this.codeGenerators = codeGenerators;
            this.provideGeneratorsForParameters = provideGeneratorsForParameters;
            this.lazy = lazy;
        }
        protected override GeneratorProvidingTypeGeneratorsBase GetActual(ExportContext context)
        {
            return new ContainedGeneratorProvidingTypeGenerator(codeGenerators, context, provideGeneratorsForParameters, lazy);
        }
    }
}
