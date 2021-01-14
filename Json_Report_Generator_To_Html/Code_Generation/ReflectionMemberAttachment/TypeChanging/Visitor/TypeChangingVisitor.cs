using Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment.RtNodeWrappers;
using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment
{
    public class TypeChangingVisitor : ReflectionGeneratedTypeVisitor
    {
        protected CompilationUnitsManager compilationUnitsManager;
        private readonly IReflectionTypeChanger typeChanger;

        public TypeChangingVisitor(ExportContext exportContext,IReflectionTypeChanger typeChanger) : base(exportContext)
        {
            this.typeChanger = typeChanger;
            this.typeChanger.ExportContext = exportContext;
        }

        public override void Visit(RtNamespace rtNamespace)
        {
            compilationUnitsManager = new CompilationUnitsManager(rtNamespace.CompilationUnits);
            typeChanger.CompilationUnitsManager = compilationUnitsManager;
            base.Visit(rtNamespace);
            compilationUnitsManager.Complete();
        }

        protected override void ReflectionConstructor(ReflectionAttachedRtConstructor constructor)
        {
            ChangeParameters(constructor.ConstructorInfo, constructor.Arguments, (type, rtTypeName, rtArgument) => typeChanger.ChangeConstructorParameterType(type, rtTypeName, rtArgument, constructor));
        }

        protected override void ReflectionField(ReflectionAttachedRtField field)
        {
            var fieldType = field.FieldInfo.FieldType;
            var changedType = typeChanger.ChangeFieldType(fieldType, field.Type, field);
            if (changedType != null)
            {
                field.Type = changedType;
            }
        }
        private List<(Type parameterType,RtArgument rtArgument)> CorrespondParametersAndRtArguments(TypeBlueprint typeBluePrint,ParameterInfo[] parameters,List<RtArgument> rtArguments)
        {
            if (parameters.Length == rtArguments.Count)
            {
                return parameters.Select((p, i) => (p.ParameterType, rtArguments[i])).ToList();
            }

            var corresponding = new List<(Type parameterType, RtArgument rtArgument)>();
            var argumentPosition = 0;
            foreach(var parameter in parameters)
            {
                if (!typeBluePrint.IsIgnored(parameter))
                {
                    corresponding.Add((parameter.ParameterType, rtArguments[argumentPosition]));
                    argumentPosition++;
                }
            }
            return corresponding;
        }
        
        private void ChangeParameters(MethodBase methodBase, List<RtArgument> rtArguments,Func<Type,RtTypeName,RtArgument,RtTypeName> typeChanger)
        {
            var typeBlueprint = exportContext.Project.Blueprint(methodBase.DeclaringType);
            var corresponding = CorrespondParametersAndRtArguments(typeBlueprint, methodBase.GetParameters(), rtArguments);
            foreach (var correspondance in corresponding)
            {
                var argument = correspondance.rtArgument;
                var changedParameterType = typeChanger(correspondance.parameterType, argument.Type, argument);
                if (changedParameterType != null)
                {
                    argument.Type = changedParameterType;
                }
            }
        }
        
        protected override void ReflectionFunction(ReflectionAttachedRtFunction function)
        {
            var methodInfo = function.MethodInfo;
            var changedReturnType = typeChanger.ChangeFunctionReturnType(methodInfo.ReturnType, function.ReturnType, function);
            if (changedReturnType != null)
            {
                function.ReturnType = changedReturnType;
            }

            ChangeParameters(methodInfo, function.Arguments, (type, rtTypeName, rtArgument) => typeChanger.ChangeFunctionParameterType(type, rtTypeName, rtArgument, function));
        }

        protected override void ReflectionProperty(ReflectionAttachedRtField property)
        {
            var propertyType = property.PropertyInfo.PropertyType;
            var changedType = typeChanger.ChangePropertyType(propertyType, property.Type,property);
            if(changedType != null)
            {
                property.Type = changedType;
            }
        }

        protected override void ReflectionParameter(ReflectionAttachedRtArgument argument)
        {
            
        }
    }

}
