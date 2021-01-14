using Reinforced.Typings;
using Reinforced.Typings.Ast;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public interface IGeneratorProvidingTypeGeneratorForMembers
    {
        void GenerateMembers<T>(Type element, TypeResolver resolver, ITypeMember typeMember, IEnumerable<T> members, ExportContext exportContext) where T : MemberInfo;
    }

}
