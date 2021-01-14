using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Json_Report_Generator_To_Html
{
    class ReporterGenerator
    {
        private readonly string exePath;
        public ReporterGenerator()
        {
            exePath = CoverletToolExePath.Get("reportGenerator");
        }
        public string GenerateCustomJsonSummaryReport(string[] coberturaFiles, string targetDirectory)
        {
            var customReportBuilderPath = typeof(FccJsonSummaryReportBuilder.FccJsonSummaryReportBuilder).Assembly.Location;
            GenerateReport(coberturaFiles, targetDirectory, new string[] { FccJsonSummaryReportBuilder.FccJsonSummaryReportBuilder.REPORT_TYPE},new string[] { customReportBuilderPath });
            return Path.Combine(targetDirectory, "Summary.json");
        }
        public string GenerateJsonSummaryReport(string[] coberturaFiles, string targetDirectory)
        {
            GenerateReport(coberturaFiles, targetDirectory, new string[] { "JsonSummary"});
            return Path.Combine(targetDirectory, "Summary.json");
        }
        public void GenerateReport(string[] coberturaFiles, string targetDir,IEnumerable<string> reportTypes,IEnumerable<string> plugins = null)
        {
            if (plugins == null)
            {
                plugins = Enumerable.Empty<string>();
            }

            var verbosity = "-verbosity:Verbose";
            //todo goto enumerable and GetMultipleArg
            var reports = "-reports:";
            for (var i = 0; i < coberturaFiles.Length; i++)
            {
                if (i != 0)
                {
                    reports += ";";
                }
                reports += coberturaFiles[i];
            }

            var reportTypesArg = GetReportTypes(reportTypes);
            var pluginsArg = GetPlugins(plugins);

            var reporterArgs = $@"{reports} {reportTypesArg} -targetdir:{targetDir} {pluginsArg}";

            var reporterProcessStartInfo = new ProcessStartInfo
            {
                FileName = exePath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = reporterArgs,
            };
            var reporterProcess = Process.Start(reporterProcessStartInfo);
            reporterProcess.WaitForExit();
            var standardOutput = reporterProcess.StandardOutput.ReadToEnd();
            Debug.WriteLine(standardOutput);
            var standardError = reporterProcess.StandardError.ReadToEnd();
        }
        private string GetPlugins(IEnumerable<string> plugins)
        {
            return GetMultipleArg("-plugins:", plugins);    
        }
        private string GetReportTypes(IEnumerable<string> reportTypes)
        {
            return GetMultipleArg("-reporttypes:",reportTypes);
        }

        private string GetMultipleArg(string flag,IEnumerable<string> multiple)
        {
            var arg = flag;
            var first = true;
            foreach (var reportType in multiple)
            {
                if (!first)
                {
                    arg += ";";
                }
                arg += reportType;
                first = false;
            }
            return arg;
        }
    }
}
