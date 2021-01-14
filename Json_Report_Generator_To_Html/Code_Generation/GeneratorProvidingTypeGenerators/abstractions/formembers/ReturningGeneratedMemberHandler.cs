using Reinforced.Typings.Ast;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    internal class ReturningGeneratedMemberHandler : IGeneratedMemberHandler
    {
        public RtNode GeneratedConstructor(RtNode node, ConstructorInfo constructorInfo)
        {
            return node;
        }

        public RtNode GeneratedField(RtNode node, FieldInfo fieldInfo)
        {
            return node;
        }

        public RtNode GeneratedMethod(RtNode node, MethodInfo methodInfo)
        {
            return node;
        }

        public RtNode GeneratedProperty(RtNode node, PropertyInfo propertyInfo)
        {
            return node;
        }
    }
}
