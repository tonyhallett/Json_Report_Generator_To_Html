//using Reinforced.Typings;
//using Reinforced.Typings.Generators;
//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace Json_Report_Generator_To_Html.Code_Generation
//{
//    internal class MemberCodeGeneration
//    {
//        private object methodGenerator;
//        private object propertyGenerator;
//        private object fieldGenerator;

//        private List<GeneratorType> generatedTypes = new List<GeneratorType>();
//        private readonly ExportContext exportContext;

//        private enum GeneratorType { Field, Property, Method};

//        public MemberCodeGeneration(Type declaringType, ExportContext exportContext)
//        {
//            this.exportContext = exportContext;
//            SetGeneratorTypes(declaringType);
//        }
//        private object InstantiateGenerator<T>(Type generatorType) where T:MemberInfo
//        {
//            var generator = Activator.CreateInstance(generatorType) as ITsCodeGenerator<T>;
//            generator.Context = exportContext;
//            return generator;

//        }

//        private void SetGeneratorTypes(Type declaringType)
//        {
//            var memberGeneratorAttributes = declaringType.GetCustomAttributes<MemberGeneratorAttribute>();
//            foreach (var memberGeneratorAttribute in memberGeneratorAttributes)
//            {
//                var generatorType = memberGeneratorAttribute.GeneratorType;
//                switch (memberGeneratorAttribute.MemberGeneratorType)
//                {
//                    case MemberGeneratorType.Field:
//                        SetGeneratorType(GeneratorType.Field, generatorType);
//                        break;
//                    case MemberGeneratorType.Method:
//                        SetGeneratorType(GeneratorType.Method,generatorType);
//                        break;
//                    case MemberGeneratorType.Property:
//                        SetGeneratorType(GeneratorType.Property, generatorType);
//                        break;
//                    case MemberGeneratorType.PropertyAndField:
//                        SetGeneratorType(GeneratorType.Field, memberGeneratorAttribute.GeneratorType);
//                        SetGeneratorType(GeneratorType.Property, memberGeneratorAttribute.GeneratorType);
//                        break;
//                }

//            }
//        }

//        private void SetGeneratorType(GeneratorType memberGeneratorType,Type generatorType)
//        {
//            if (generatedTypes.Contains(memberGeneratorType))
//            {
//                //throw ?
//            }
//            else
//            {
//                generatedTypes.Add(memberGeneratorType);
//                switch (memberGeneratorType)
//                {
//                    case GeneratorType.Field:
//                        fieldGenerator = InstantiateGenerator<FieldInfo>(generatorType);
//                        break;
//                    case GeneratorType.Property:
//                        propertyGenerator = InstantiateGenerator<PropertyInfo>(generatorType);
//                        break;
//                    case GeneratorType.Method:
//                        methodGenerator = InstantiateGenerator<MethodInfo>(generatorType);
//                        break;
//                }
//            }
//        }

//        public object MethodGenerator => methodGenerator;
//        public object PropertyGenerator => propertyGenerator;
//        public object FieldGenerator => fieldGenerator;
//    }

//}
