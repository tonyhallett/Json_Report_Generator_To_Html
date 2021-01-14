using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Json_Report_Generator_To_Html
{
    static class CoverletToolExePath
    {
        public static string Get(string name)
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppData, $@"FineCodeCoverage\{name}\{name}.exe");
        }
    }
}
