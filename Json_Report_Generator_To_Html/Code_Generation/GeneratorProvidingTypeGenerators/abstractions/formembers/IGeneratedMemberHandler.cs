using Reinforced.Typings.Ast;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public interface IGeneratedMemberHandler
    {
        RtNode GeneratedField(RtNode node, FieldInfo field);

        RtNode GeneratedMethod(RtNode node, MethodInfo method);
        RtNode GeneratedProperty(RtNode node, PropertyInfo propertyInfo);

        RtNode GeneratedConstructor(RtNode node, ConstructorInfo constructorInfo);
    }
}
