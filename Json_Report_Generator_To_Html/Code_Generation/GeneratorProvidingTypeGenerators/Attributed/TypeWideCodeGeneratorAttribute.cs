using System;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public enum For { Constructor, Method, Property, Field, Parameter }

    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface|AttributeTargets.Struct,AllowMultiple = true)]
    public class TypeWideCodeGeneratorAttribute : Attribute
    {
        public TypeWideCodeGeneratorAttribute(For @for, Type generatorType)
        {
            For = @for;
            GeneratorType = generatorType;
        }

        public For For { get; }
        public Type GeneratorType { get; }
    }

}
