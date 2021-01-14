using Reinforced.Typings.Ast;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment
{
    public class ReflectionGeneratedMemberHandler : IGeneratedMemberHandler
    {
        public RtNode GeneratedConstructor(RtNode node, ConstructorInfo constructorInfo)
        {
            if(node is RtConstructor)
            {
                return new ReflectionAttachedRtConstructor(node as RtConstructor, constructorInfo);
            }
            return node;
            
        }

        public RtNode GeneratedField(RtNode node, FieldInfo fieldInfo)
        {
            if (node is RtField)
            {
                return new ReflectionAttachedRtField(node as RtField, fieldInfo);
            }
            return node;
        }

        public RtNode GeneratedMethod(RtNode node, MethodInfo methodInfo)
        {
            if (node is RtFunction)
            {
                return new ReflectionAttachedRtFunction(node as RtFunction, methodInfo);
            }
            return node;
        }

        public RtNode GeneratedProperty(RtNode node, PropertyInfo propertyInfo)
        {
            if (node is RtField)
            {
                return new ReflectionAttachedRtField(node as RtField, propertyInfo);
            }
            return node;
        }
    }
}
