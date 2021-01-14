using FccJsonSummaryReportBuilder;
using Json_Report_Generator_To_Html.JsonSummary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Json_Report_Generator_To_Html
{
    class Program
    {
        private const string targetDirectory = @"C:\Users\tonyh\Downloads\report_generator_json_to_html";
        private static string jsonToHtmlPath => Path.Combine(targetDirectory, "index.html");
        private static string[] coverageFiles = new string[]
        {
            @"C:\Users\tonyh\Source\Repos\XUnit_Conditional_Fact\Test\bin\Debug\netcoreapp3.1\fine-code-coverage\project.coverage.xml"
        };
        private static ReporterGenerator reportGenerator = new ReporterGenerator();

        static void Main(string[] args)
        {
            JsonSummary();            
        }
        private static void JsonSummary()
        {
            var jsonPath = reportGenerator.GenerateJsonSummaryReport(coverageFiles, targetDirectory);
            try
            {
                var deserialized = JsonSerializer.Deserialize<JsonSummaryResult>(File.ReadAllText(jsonPath));
                var st2 = "";
            }
            catch (Exception exc)
            {
                var st = "";
            }
        }
        private static void CustomJsonSummary()
        {
            var jsonPath = reportGenerator.GenerateCustomJsonSummaryReport(coverageFiles, targetDirectory);
            try
            {
                var deserialized = JsonSerializer.Deserialize<CustomJsonCoverageResult>(File.ReadAllText(jsonPath));
            }
            catch (Exception exc)
            {
                var st = "";
            }

            WriteCustomJsonHtmlFile(jsonPath, jsonToHtmlPath);
        }
        

        private static void WriteCustomJsonHtmlFile(string jsonPath,string htmlPath)
        {
            var jsonString = File.ReadAllText(jsonPath);

            var html = $@"
<!doctype html>
<html lang=""en"">
<head>
</head>
<body>
    <script>
        var summaryResult = {jsonString};
        alert(summaryResult.Summary.Parser);
    </script>
</body>
</html> 
            ";
            File.WriteAllText(htmlPath, html);
               
        }
    }

    
}
