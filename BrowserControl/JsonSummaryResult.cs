using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BrowserControl.Deserialization
{
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

}
