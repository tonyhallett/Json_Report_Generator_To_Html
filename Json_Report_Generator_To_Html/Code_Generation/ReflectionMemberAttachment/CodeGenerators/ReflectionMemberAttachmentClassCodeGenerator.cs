using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Generators;
using System;
using System.Collections.Generic;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public class ReflectionMemberAttachmentClassCodeGenerator : ClassCodeGenerator
    {
        private ReflectionMemberAttachmentGenerator memberAttachmentGenerator = new ReflectionMemberAttachmentGenerator();
        protected override void GenerateMembers<T>(Type element, TypeResolver resolver, ITypeMember typeMember, IEnumerable<T> members)
        {
            memberAttachmentGenerator.GenerateMembers(resolver, typeMember, members, Context.Generators);
        }
    }

}
