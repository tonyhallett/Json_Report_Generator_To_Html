using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserControl
{
    class CoverletGlobal
    {
        private string exePath;

        public CoverletGlobal()
        {
            exePath = CoverletToolExePath.Get("coverlet");
        }


        public List<string> RunCobertura(List<string> dlls, string coverletOutput)
        {
            var coberturaFiles = new List<string>();
            for (var i = 0; i < dlls.Count; i++)
            {
                var path = dlls[i];
                var output = $@"{coverletOutput}\cobertura{i}.cobertura";
                coberturaFiles.Add(output);
                var coverletArgs = $"\"{path}\" --format \"cobertura\" --include-test-assembly --target \"dotnet\" --output \"{output}\" --targetargs \"test {path}\"";
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = coverletArgs,
                };
                var process = Process.Start(processStartInfo);
                process.WaitForExit();
            }
            return coberturaFiles;
        }
    }
}
