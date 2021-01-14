using Json_Report_Generator_To_Html.Code_Generation;
using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Attributes;
using Reinforced.Typings.Generators;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Json_Report_Generator_To_Html.JsonSummary
{
    //public interface DemoWithMethod
    //{
    //    decimal? NullableReturn();
    //    void NullableArguments(int? nullableInt, bool? nullableBool, DateTime? nullableDateTime);
    //    void NullableExcludeArguments(int? nullableInt, bool? nullableBool,[TsIgnore] DateTime? nullableDateTimeIgnored);
    //    List<decimal?> NullableArray();
    //}
    //public class IsGeneric<T>
    //{

    //}
    //public class ClassToClass
    //{
    //    private readonly int? nullableInt;
    //    private readonly bool? nullableBool;
    //    private readonly DateTime? nullableDateTimeIgnored;


    //    public ClassToClass(int? nullableInt, bool? nullableBool, [TsIgnore] DateTime? nullableDateTimeIgnored)
    //    {
    //        this.nullableInt = nullableInt;
    //        this.nullableBool = nullableBool;
    //        this.nullableDateTimeIgnored = nullableDateTimeIgnored;
    //    }
    //}

    //[ExcludeTypeFromFluent]
    //public class ExcludeThis
    //{
    //    public bool ExcludedMethod() { return true; }
    //}


    public class JsonSummaryResult
    {
        public JsonCoverageSummary summary { get; set; }
        public JsonCoverageCoverage coverage { get; set; }
    }

    public class JsonCoverageSummary
    {
        public DateTime generatedon { get; set; }
        public string parser { get; set; }
        public int assemblies { get; set; }
        public int classes { get; set; }
        public int files { get; set; }

        public int coveredlines { get; set; }
        public int uncoveredlines { get; set; }
        public int coverablelines { get; set; }
        public int totallines { get; set; }
        public decimal? linecoverage { get; set; }
        public int coveredbranches { get; set; }
        public int totalbranches { get; set; }
        public decimal? branchcoverage { get; set; }
    }
    
    public class JsonCoverageCoverage
    {
        public List<JsonAssemblyCoverage> assemblies { get; set; }
    }

    public class JsonAssemblyCoverage
    {
        public string name { get; set; }
        public int classes { get; set; }

        public int coveredlines { get; set; }
        public int coverablelines { get; set; }
        public int totallines { get; set; }
        public decimal? coverage { get; set; }//line
        public int coveredbranches { get; set; }
        public int totalbranches { get; set; }
        public decimal? branchcoverage { get; set; }
        public List<JsonClassCoverage> classesinassembly { get; set; }
    }

    public class JsonClassCoverage
    {
        public string name { get; set; }
        public int coveredlines { get; set; }
        public int uncoveredlines { get; set; }
        public int coverablelines { get; set; }
        public int totallines { get; set; }
        public decimal? coverage { get; set; }
        public int coveredbranches { get; set; }
        public int totalbranches { get; set; }
        public decimal? branchcoverage { get; set; }
    }

    //[AttributedGeneratorInterface]
    //[MemberGenerator(MemberGeneratorType.Property,typeof(DemoPropertyGenerator))]
    //[MemberGenerator(MemberGeneratorType.Method, typeof(DemoMethodGenerator))]
    //public class DemoClassToInterfaceWithCustomGenerators
    //{
    //    public string Property1 { get; set; }
    //    public string Property2 { get; set; }

    //    [TsProperty(CodeGeneratorType = typeof(SpecificiedPropertyGenerator))]
    //    public string Specified { get; set; }

    //    [TsProperty]
    //    public string NotSpecified { get; set; }

    //    public void Method1() { }
    //    public void Method2() { }
    //}

    //public class DemoPropertyGenerator: PropertyCodeGenerator
    //{
    //    public override RtField GenerateNode(MemberInfo element, RtField result, TypeResolver resolver)
    //    {
    //        Context.Warnings.Add(new Reinforced.Typings.Exceptions.RtWarning(0, null, $"Hello {element.Name}"));
    //        return base.GenerateNode(element, result, resolver);
    //    }
    //}

    //public class SpecificiedPropertyGenerator : PropertyCodeGenerator
    //{
    //    public override RtField GenerateNode(MemberInfo element, RtField result, TypeResolver resolver)
    //    {
    //        Context.Warnings.Add(new Reinforced.Typings.Exceptions.RtWarning(0, null, $"Specified {element.Name}"));
    //        return base.GenerateNode(element, result, resolver);
    //    }
    //}

    //public class DemoMethodGenerator : MethodCodeGenerator
    //{
    //    public override RtFunction GenerateNode(MethodInfo element, RtFunction result, TypeResolver resolver)
    //    {
    //        Context.Warnings.Add(new Reinforced.Typings.Exceptions.RtWarning(0, null, $"Hello {element.Name}"));
    //        return base.GenerateNode(element, result, resolver);
    //    }
    //}
}
