using Json_Report_Generator_To_Html.Code_Generation.GeneratorProvidingTypeGenerators.Contained;
using Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment;
using Json_Report_Generator_To_Html.JsonSummary;
using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using Reinforced.Typings.Generators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public static class ReinforcedTypingsConfiguration
    {
        private static (INullableConfiguration inferenceConfiguration, INullableConfiguration visitorConfiguration) GetConfigurators()
        {
            return (new NullableInferenceConfiguration(), new NullableReflectionVisitorConfiguration());
        }
        private static INullableConfiguration DetermineConfiguration()
        {
            var configurators = GetConfigurators();
            return configurators.inferenceConfiguration;//switch
        }
        private static void ConfigureTypes(this ConfigurationBuilder builder,Action<ClassOrInterfaceExportBuilder> classOrInterfaceConfiguration)
        {
            var exportAllInNamespaceAsInterfaces = true;
            if (exportAllInNamespaceAsInterfaces)
            {
                builder.ExportAllInNamespaceAsInterfaces(typeof(JsonSummaryResult), classOrInterfaceConfiguration);
            }
            else
            {
                // this does not work
                //classOrInterfaceConfiguration(builder.ExportAsInterface<IsGeneric<object>>());
                //builder.ExportAsClasses(new Type[] { typeof(IsGeneric<>) }, ceb => ceb.WithConstructor());
            }
        }
        public static void Configure(ConfigurationBuilder builder)
        {
            //var configuration = DetermineConfiguration();
            //configuration.Configure(builder);

            //builder.ConfigureTypes(classOrInterfaceBuilder =>
            //{
            //    configuration.ConfigureTypeBuilder(classOrInterfaceBuilder);
            //}); 

            builder.Substitute(typeof(DateTime), new RtSimpleTypeName("Date"));
            builder.ExportAllInNamespaceAsInterfaces(typeof(JsonSummaryResult), interfaceBuilder =>
             {
                 interfaceBuilder.WithCodeGenerator<InterfaceExportBuilder, CommentWritingContainedInterfaceGeneratorProvidingGenerators>().WithAllMembers();
             });
        }
        
    }
    internal class NullableCommentWritingPropertyCodeGenerator : PropertyCodeGenerator
    {
        public override RtField GenerateNode(MemberInfo element, RtField result, TypeResolver resolver)
        {
            var generated = base.GenerateNode(element, result, resolver);
            var propertyType = (element as PropertyInfo).PropertyType;
            if (propertyType.IsNullable())
            {
                generated.Documentation = new RtJsdocNode() { Description = "Nullable" };
            }
            return generated;
        }
    }
    public class CommentWritingContainedInterfaceGeneratorProvidingGenerators : ContainedInterfaceGeneratorProvidingGenerators
    {
        public CommentWritingContainedInterfaceGeneratorProvidingGenerators() : base(new Dictionary<Type, Type>
        {
            { typeof(PropertyInfo), typeof(NullableCommentWritingPropertyCodeGenerator)}
        })
        {

        }
    }


    internal interface INullableConfiguration
    {
        void ConfigureTypeBuilder(ClassOrInterfaceExportBuilder classOrInterfaceExportBuilder);
        void Configure(ConfigurationBuilder configurationBuilder);
    }
    class NullableInferenceConfiguration : INullableConfiguration
    {

        private NullableInferer nullableInferer;
        private ExportContext exportContext;

        public void Configure(ConfigurationBuilder configurationBuilder)
        {
            exportContext = configurationBuilder.Context;
            nullableInferer = new NullableInferer(exportContext);
        }

        public void ConfigureTypeBuilder(ClassOrInterfaceExportBuilder classOrInterfaceExportBuilder)
        {
            classOrInterfaceExportBuilder.WithAllWithInference(nullableInferer, exportContext);
        }
    }

    class NullableReflectionVisitorConfiguration : INullableConfiguration
    {
        public void Configure(ConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Global(c => c.UseVisitor<NullableTypeTypeScriptExportVisitor>());
        }

        public void ConfigureTypeBuilder(ClassOrInterfaceExportBuilder classOrInterfaceExportBuilder)
        {
            if(classOrInterfaceExportBuilder is ClassExportBuilder)
            {
                classOrInterfaceExportBuilder.WithCodeGenerator<ClassOrInterfaceExportBuilder, ReflectionMemberAttachmentClassCodeGenerator>().
                    WithAllMembers();
            }
            else
            {
                classOrInterfaceExportBuilder.WithCodeGenerator<ClassOrInterfaceExportBuilder, ReflectionMemberAttachmentInterfaceCodeGenerator>().
                    WithAllMembers();
            }
            
        }
    }
}
