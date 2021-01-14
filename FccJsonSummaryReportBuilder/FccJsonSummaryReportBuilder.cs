using Newtonsoft.Json;
using Palmmedia.ReportGenerator.Core.Logging;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using Palmmedia.ReportGenerator.Core.Reporting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//using System.Text.Json;

namespace FccJsonSummaryReportBuilder
{
    public class CustomJsonCoverageResult
    {
        public JsonCoverageSummary Summary { get; set; }
        public JsonCoverageCoverage Coverage { get; set; }

        public static CustomJsonCoverageResult Create(SummaryResult summaryResult)
        {
            return new CustomJsonCoverageResult
            {
                Coverage = new JsonCoverageCoverage { },
                Summary = new JsonCoverageSummary { 
                    GeneratedOn = DateTime.Now.ToUniversalTime(),
                    Parser = summaryResult.UsedParser
                }
            };
        }
    }
    public class JsonCoverageSummary
    {
        public DateTime GeneratedOn { get; set; }
        public string Parser { get; set; }
        //public int assemblies { get; set; }
        //public int classes { get; set; }
        //public int files { get; set; }

        //public int coveredlines { get; set; }
        //public int uncoveredlines { get; set; }
        //public int coverablelines { get; set; }
        //public int totallines { get; set; }
        //public decimal linecoverage { get; set; }
        //public int coveredbranches { get; set; }
        //public int totalbranches { get; set; }
        //public decimal branchcoverage { get; set; }
    }
    public class JsonCoverageCoverage
    {
        public List<JsonAssemblyCoverage> Assemblies { get; set; }
    }
    public class JsonAssemblyCoverage
    {
        public string name { get; set; }
        public int classes { get; set; }

        public int coveredlines { get; set; }
        //public int Uncoveredlines { get; set; }
        public int coverablelines { get; set; }
        public int totallines { get; set; }
        public decimal coverage { get; set; }//line
        public int coveredbranches { get; set; }
        public int totalbranches { get; set; }
        public decimal branchcoverage { get; set; }
        public List<JsonClassCoverage> classesinassembly { get; set; }
    }

    public class JsonClassCoverage
    {
        public string name { get; set; }
        public int coveredlines { get; set; }//
        public int uncoveredlines { get; set; }
        public int coverablelines { get; set; }//
        public int totallines { get; set; }//
        public decimal coverage { get; set; }//line
        public int coveredbranches { get; set; }//
        public int totalbranches { get; set; }//
        public decimal branchcoverage { get; set; }// - "branchcoverage": null, *******************
        //"coverablelines": 22, "totallines": 67, "branchcoverage": null, "coveredbranches": 0, "totalbranches": 0 
    }

    public class FccJsonSummaryReportBuilder : IReportBuilder
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger(typeof(FccJsonSummaryReportBuilder));
        public string ReportType => REPORT_TYPE;
        public static string REPORT_TYPE = "FccJsonSummary";
        public static string OutputFile = "Summary.json";
        public IReportContext ReportContext { get; set; }

        public void CreateClassReport(Class @class, IEnumerable<FileAnalysis> fileAnalyses)
        {
            
        }

        public void CreateSummaryReport(SummaryResult summaryResult)
        {
            string targetDirectory = this.ReportContext.ReportConfiguration.TargetDirectory;

            if (this.ReportContext.Settings.CreateSubdirectoryForAllReportTypes)
            {
                targetDirectory = Path.Combine(targetDirectory, this.ReportType);

                if (!Directory.Exists(targetDirectory))
                {
                    try
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }
                    catch (Exception ex)
                    {
                        //todo
                        //Logger.ErrorFormat(Resources.TargetDirectoryCouldNotBeCreated, targetDirectory, ex.GetExceptionMessageForDisplay());
                        return;
                    }
                }
            }
            Logger.Info("Hello !");

            string targetPath = Path.Combine(targetDirectory, OutputFile);
            var jsonResult = CustomJsonCoverageResult.Create(summaryResult);
            var result = JsonConvert.SerializeObject(jsonResult);
            File.WriteAllText(targetPath, result);

            //todo
            //Logger.InfoFormat(Resources.WritingReportFile, targetPath);

            /*
                

            

            using (var reportTextWriter = new StreamWriter(new FileStream(targetPath, FileMode.Create), Encoding.UTF8))
            {
                reportTextWriter.WriteLine("{");
                reportTextWriter.WriteLine("  \"summary\": {");
                reportTextWriter.WriteLine($"    \"generatedon\": \"{DateTime.Now.ToUniversalTime().ToString("s")}Z\",");
                reportTextWriter.WriteLine($"    \"parser\": \"{summaryResult.UsedParser}\",");
                reportTextWriter.WriteLine($"    \"assemblies\": {summaryResult.Assemblies.Count().ToString(CultureInfo.InvariantCulture)},");
                reportTextWriter.WriteLine($"    \"classes\": {summaryResult.Assemblies.SelectMany(a => a.Classes).Count().ToString(CultureInfo.InvariantCulture)},");
                reportTextWriter.WriteLine($"    \"files\": {summaryResult.Assemblies.SelectMany(a => a.Classes).SelectMany(a => a.Files).Distinct().Count().ToString(CultureInfo.InvariantCulture)},");
                reportTextWriter.WriteLine($"    \"coveredlines\": {summaryResult.CoveredLines.ToString(CultureInfo.InvariantCulture)},");
                reportTextWriter.WriteLine($"    \"uncoveredlines\": {(summaryResult.CoverableLines - summaryResult.CoveredLines).ToString(CultureInfo.InvariantCulture)},");
                reportTextWriter.WriteLine($"    \"coverablelines\": {summaryResult.CoverableLines.ToString(CultureInfo.InvariantCulture)},");
                reportTextWriter.WriteLine($"    \"totallines\": {summaryResult.TotalLines.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)},");

                if (summaryResult.CoverageQuota.HasValue)
                {
                    reportTextWriter.Write($"    \"linecoverage\": {summaryResult.CoverageQuota.Value.ToString(CultureInfo.InvariantCulture)}");
                }
                else
                {
                    reportTextWriter.Write($"    \"linecoverage\": null");
                }

                if (summaryResult.CoveredBranches.HasValue && summaryResult.TotalBranches.HasValue)
                {
                    reportTextWriter.WriteLine(",");
                    reportTextWriter.WriteLine($"    \"coveredbranches\": {summaryResult.CoveredBranches.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)},");
                    reportTextWriter.Write($"    \"totalbranches\": {summaryResult.TotalBranches.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)}");

                    if (summaryResult.BranchCoverageQuota.HasValue)
                    {
                        reportTextWriter.WriteLine(",");
                        reportTextWriter.Write($"    \"branchcoverage\": {summaryResult.BranchCoverageQuota.Value.ToString(CultureInfo.InvariantCulture)}");
                    }
                }

                reportTextWriter.WriteLine(" },");

                var sumableMetrics = summaryResult.SumableMetrics;

                if (sumableMetrics.Count > 0)
                {
                    reportTextWriter.WriteLine("  \"metrics\": [");

                    int metricCounter = 0;

                    foreach (var metric in sumableMetrics)
                    {
                        if (metricCounter > 0)
                        {
                            reportTextWriter.WriteLine(",");
                        }

                        if (metric.Value.HasValue)
                        {
                            reportTextWriter.Write($"    {{ \"name\": \"{JsonSerializer.EscapeString(metric.Name)}\", \"value\": {metric.Value.Value.ToString(CultureInfo.InvariantCulture)} }}");
                        }
                        else
                        {
                            reportTextWriter.Write($"    {{ \"name\": \"{JsonSerializer.EscapeString(metric.Name)}\", \"value\": null }}");
                        }

                        metricCounter++;
                    }

                    reportTextWriter.WriteLine(" ],");
                }

                reportTextWriter.WriteLine("  \"coverage\": {");

                reportTextWriter.WriteLine("    \"assemblies\": [");

                int assemblyCounter = 0;

                foreach (var assembly in summaryResult.Assemblies)
                {
                    if (assemblyCounter > 0)
                    {
                        reportTextWriter.WriteLine(",");
                    }

                    reportTextWriter.WriteLine($"      {{ \"name\": \"{JsonSerializer.EscapeString(assembly.Name)}\", \"classes\": {assembly.Classes.Count().ToString(CultureInfo.InvariantCulture)}, \"coverage\": {(assembly.CoverageQuota.HasValue ? assembly.CoverageQuota.Value.ToString(CultureInfo.InvariantCulture) : "null")}, \"coveredlines\": {assembly.CoveredLines.ToString(CultureInfo.InvariantCulture)}, \"coverablelines\": {assembly.CoverableLines.ToString(CultureInfo.InvariantCulture)}, \"totallines\": {(assembly.TotalLines.HasValue ? assembly.TotalLines.Value.ToString(CultureInfo.InvariantCulture) : "null")}, \"branchcoverage\": {(assembly.BranchCoverageQuota.HasValue ? assembly.BranchCoverageQuota.Value.ToString(CultureInfo.InvariantCulture) : "null")}, \"coveredbranches\": {(assembly.CoveredBranches.HasValue ? assembly.CoveredBranches.Value.ToString(CultureInfo.InvariantCulture) : "null")}, \"totalbranches\": {(assembly.TotalBranches.HasValue ? assembly.TotalBranches.Value.ToString(CultureInfo.InvariantCulture) : null)}, \"classesinassembly\": [");

                    int classCounter = 0;

                    foreach (var @class in assembly.Classes)
                    {
                        if (classCounter > 0)
                        {
                            reportTextWriter.WriteLine(",");
                        }

                        reportTextWriter.Write($"        {{ \"name\": \"{JsonSerializer.EscapeString(@class.Name)}\", \"coverage\": {(@class.CoverageQuota.HasValue ? @class.CoverageQuota.Value.ToString(CultureInfo.InvariantCulture) : "null")}, \"coveredlines\": {@class.CoveredLines.ToString(CultureInfo.InvariantCulture)}, \"coverablelines\": {@class.CoverableLines.ToString(CultureInfo.InvariantCulture)}, \"totallines\": {(@class.TotalLines.HasValue ? @class.TotalLines.Value.ToString(CultureInfo.InvariantCulture) : "null")}, \"branchcoverage\": {(@class.BranchCoverageQuota.HasValue ? @class.BranchCoverageQuota.Value.ToString(CultureInfo.InvariantCulture) : "null")}, \"coveredbranches\": {(@class.CoveredBranches.HasValue ? @class.CoveredBranches.Value.ToString(CultureInfo.InvariantCulture) : "null")}, \"totalbranches\": {(@class.TotalBranches.HasValue ? @class.TotalBranches.Value.ToString(CultureInfo.InvariantCulture) : null)} }}");

                        classCounter++;
                    }

                    reportTextWriter.Write(" ] }");

                    assemblyCounter++;
                }

                reportTextWriter.WriteLine(" ]");

                reportTextWriter.WriteLine("  }");
                reportTextWriter.Write("}");

                reportTextWriter.Flush();
            }
        }
            */
        }
    }
}
