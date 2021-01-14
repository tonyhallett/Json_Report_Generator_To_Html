using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Generators;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public class ReflectionMemberAttachmentInterfaceCodeGenerator : InterfaceCodeGenerator
    {
        private ReflectionMemberAttachmentGenerator memberAttachmentGenerator = new ReflectionMemberAttachmentGenerator();
        protected override void GenerateMembers<T>(Type element, TypeResolver resolver, ITypeMember typeMember, IEnumerable<T> members)
        {
            memberAttachmentGenerator.GenerateMembers(resolver, typeMember, members, Context.Generators);
        }
    }

}
