using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserControl
{
    class Reporter
    {
        private readonly string exePath;
        public Reporter()
        {
            exePath = CoverletToolExePath.Get("reportGenerator");
        }
        public string GenerateJsonReport(List<string> coberturaFiles, string targetDirectory)
        {
            var reports = "-reports:";
            for (var i = 0; i < coberturaFiles.Count; i++)
            {
                if (i != 0)
                {
                    reports += ";";
                }
                reports += coberturaFiles[i];
            }
            var reportTypes = "-reporttypes:JsonSummary";
            var reporterArgs = $@"-targetdir:{targetDirectory} {reportTypes} {reports}";
            var reporterProcessStartInfo = new ProcessStartInfo
            {
                FileName = exePath,
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = reporterArgs,
            };
            var reporterProcess = Process.Start(reporterProcessStartInfo);
            reporterProcess.WaitForExit();

            return Path.Combine(targetDirectory, "Summary.json");
        }
    }
}
