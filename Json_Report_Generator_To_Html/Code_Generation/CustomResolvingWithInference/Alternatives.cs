using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Attributes;
using Reinforced.Typings.Fluent;
using Reinforced.Typings.Generators;
using System;
using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public class NullablePropertyCodeGenerator : PropertyCodeGenerator
    {
        private bool ProducedNullableType;
        public override RtField GenerateNode(MemberInfo element, RtField result, TypeResolver resolver)
        {
            if (!ProducedNullableType)
            {
                if (element.GetCustomAttribute(typeof(NullablePropertyAttribute)) != null)
                {
                    if (Context.Location.CurrentNamespace != null)
                    {
                        Context.Location.CurrentNamespace.CompilationUnits.Add(new RtRaw("type Nullable<T> = T | null"));
                        ProducedNullableType = true;
                    }
                }
            }
            // actually no need for the attribute - can use the Type property - RtTypeName and change it
            return base.GenerateNode(element, result, resolver);
        }
    }

    public class NullablePropertyAttribute : TsPropertyAttribute
    {
        public NullablePropertyAttribute()
        {
            this.CodeGeneratorType = typeof(NullablePropertyCodeGenerator);
            this.InferType((mi, tr) =>
            {
                var propertyInfo = mi as PropertyInfo;
                var type = propertyInfo.PropertyType;
                var resolvedTypeName = tr.ResolveTypeName(type);
                if (type.IsNullable())
                {
                    var simpleName = resolvedTypeName as RtSimpleTypeName;
                    if ( simpleName != null)
                    {
                        return new RtSimpleTypeName($"Nullable<{simpleName.TypeName}>");
                    }
                }
                
                return resolvedTypeName;

            });
        }
    }

    /*
        could have done with TsProperty.Type but this is better
        Even better is with fluent configuration
        https://github.com/reinforced/Reinforced.Typings/wiki/Code-Generators#with-fluent-configuration
        public static void ConfigureTypings(ConfigurationBuilder builder)


        public static ConfigurationBuilder Substitute(this ConfigurationBuilder builder, Type substitute,
			RtTypeName substitution)

    */

    // note that visitor no good for changing types as keeps no record of orginal c#
    // Not possible for InterfaceCodeGenerator to add own generator - attributes seem only way.

    public class InterfaceTypeMappingCodeGenerator : InterfaceCodeGenerator
    {
        private bool ProducedNullableType;
        public override RtInterface GenerateNode(Type element, RtInterface result, TypeResolver resolver)
        {
            Context.CurrentBlueprint.Substitutions.Add(typeof(DateTime), new RtSimpleTypeName("Date"));
            // could add nullables for all simple types but that is really not good enough
            // need a way to provide own resolver but this does not appear possible
            var nullableSubstitutions = new Type[]
            {
                typeof(bool?),
                typeof(char?),
                typeof(byte?),
                typeof(sbyte?),
                typeof(short?),
                typeof(ushort?),
                typeof(int?),
                typeof(uint?),
                typeof(long?),
                typeof(ulong?),
                typeof(float?),
                typeof(double?),
                typeof(decimal?),
                typeof(DateTime?)
            };
            
            foreach(var nullableSubstitution in nullableSubstitutions)
            {
                Context.CurrentBlueprint.Substitutions.Add(nullableSubstitution, new RtSimpleTypeName($"Nullable<{resolver.ResolveTypeName(nullableSubstitution)}>"));
            }

            // with this method do not know if there has been any substitutions

            if (!ProducedNullableType && Context.Location.CurrentNamespace != null)
            {
                Context.Location.CurrentNamespace.CompilationUnits.Add(new RtRaw("type Nullable<T> = T | null"));
                ProducedNullableType = true;
            }

            return base.GenerateNode(element, result, resolver);
        }
        
    }
}
