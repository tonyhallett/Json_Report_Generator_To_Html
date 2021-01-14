using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Generators;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment.CodeGenerators
{
    class ReflectionAttachmentParameterCodeGenerator: ParameterCodeGenerator
    {
        public override RtArgument GenerateNode(ParameterInfo element, RtArgument result, TypeResolver resolver)
        {
            return base.GenerateNode(element, result, resolver);
        }
    }
}
