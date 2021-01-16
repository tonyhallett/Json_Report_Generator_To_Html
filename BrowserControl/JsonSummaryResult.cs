using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml.Linq;

namespace BrowserControl.Deserialization
{
    /*
        understand why this is not working
        https://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/WebBrowser.cs,92ef4df78ad4750d,references
    */
    [System.Runtime.InteropServices.ComVisible(true)]
    public class JsonSummaryResult
    {
        public JsonCoverageSummary summary;
        public JsonCoverageCoverage coverage;
    }

    [System.Runtime.InteropServices.ComVisible(true)]
    public class JsonCoverageSummary
    {
        public DateTime generatedon;
        public string parser;
        public int assemblies;
        public int classes;
        public int files;

        public int coveredlines;
        public int uncoveredlines;
        public int coverablelines;
        public int totallines;
        public decimal? linecoverage;
        public int coveredbranches;
        public int totalbranches;
        public decimal? branchcoverage;
    }

    [System.Runtime.InteropServices.ComVisible(true)]
    public class JsonCoverageCoverage
    {
        public JsonAssemblyCoverage[] assemblies;
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class JsonAssemblyCoverage
    {
        public string name;
        public int classes;

        public int coveredlines;
        public int coverablelines;
        public int totallines;
        public decimal? coverage;//line
        public int coveredbranches;
        public int totalbranches;
        public decimal? branchcoverage;
        public JsonClassCoverage[] classesinassembly;
    }

    [System.Runtime.InteropServices.ComVisible(true)]
    public class JsonClassCoverage
    {
        public string name;
        public int coveredlines;
        public int uncoveredlines;
        public int coverablelines;
        public int totallines;
        public decimal? coverage;
        public int coveredbranches;
        public int totalbranches;
        public decimal? branchcoverage;
    }
    //public class JsonSummaryResult
    //{
    //    public JsonCoverageSummary summary { get; set; }
    //    public JsonCoverageCoverage coverage { get; set; }
    //}
    //public class JsonCoverageSummary
    //{
    //    public DateTime generatedon { get; set; }
    //    public string parser { get; set; }
    //    public int assemblies { get; set; }
    //    public int classes { get; set; }
    //    public int files { get; set; }

    //    public int coveredlines { get; set; }
    //    public int uncoveredlines { get; set; }
    //    public int coverablelines { get; set; }
    //    public int totallines { get; set; }
    //    public decimal? linecoverage { get; set; }
    //    public int coveredbranches { get; set; }
    //    public int totalbranches { get; set; }
    //    public decimal? branchcoverage { get; set; }
    //}
    //public class JsonCoverageCoverage
    //{
    //    public List<JsonAssemblyCoverage> assemblies { get; set; }
    //}
    //public class JsonAssemblyCoverage
    //{
    //    public string name { get; set; }
    //    public int classes { get; set; }

    //    public int coveredlines { get; set; }
    //    public int coverablelines { get; set; }
    //    public int totallines { get; set; }
    //    public decimal? coverage { get; set; }//line
    //    public int coveredbranches { get; set; }
    //    public int totalbranches { get; set; }
    //    public decimal? branchcoverage { get; set; }
    //    public List<JsonClassCoverage> classesinassembly { get; set; }
    //}
    //public class JsonClassCoverage
    //{
    //    public string name { get; set; }
    //    public int coveredlines { get; set; }
    //    public int uncoveredlines { get; set; }
    //    public int coverablelines { get; set; }
    //    public int totallines { get; set; }
    //    public decimal? coverage { get; set; }
    //    public int coveredbranches { get; set; }
    //    public int totalbranches { get; set; }
    //    public decimal? branchcoverage { get; set; }
    //}
}
