using Reinforced.Typings.Ast;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public class ReflectionAttachedRtFunction : RtFunction
    {
        public MethodInfo MethodInfo { get; }
        internal bool Handled { get; set; }
        public ReflectionAttachedRtFunction(RtFunction actual,MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;

            this.IsAsync = actual.IsAsync;
            this.Identifier = actual.Identifier;
            this.ReturnType = actual.ReturnType;
            foreach(var argument in actual.Arguments)
            {
                Arguments.Add(argument);
            }
            foreach (var decorator in actual.Decorators)
            {
                Decorators.Add(decorator);
            }
            this.Body = actual.Body;

            this.CopyActualProperties(actual);
        }
    }

}
