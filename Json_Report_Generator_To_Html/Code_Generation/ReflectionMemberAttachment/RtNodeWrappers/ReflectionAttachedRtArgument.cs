using Reinforced.Typings.Ast;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment.RtNodeWrappers
{
    public class ReflectionAttachedRtArgument:RtArgument
    {
        public ParameterInfo ParameterInfo { get; }

        internal bool Handled { get; set; }

        public ReflectionAttachedRtArgument(RtArgument actual, ParameterInfo parameterInfo)
        {
            ParameterInfo = parameterInfo;

            this.Identifier = actual.Identifier;
            this.DefaultValue = actual.DefaultValue;
            this.IsVariableParameters = actual.IsVariableParameters;
            this.Type = actual.Type;
            foreach(var decorator in actual.Decorators)
            {
                this.Decorators.Add(decorator);
            }
        }
    }
}
