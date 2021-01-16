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
    public class JsonSummaryResult
    {
        public JsonCoverageSummary summary;
        public JsonCoverageCoverage coverage;
    }

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

    public class JsonCoverageCoverage
    {
        public List<JsonAssemblyCoverage> assemblies;
    }

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

}
