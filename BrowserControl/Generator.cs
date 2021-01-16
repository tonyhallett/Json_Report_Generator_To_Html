using BrowserControl.Deserialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserControl
{

    class Generator : IGenerator
    {
        private readonly string outputDirectory;
        private readonly int fakeGenerationTime;
        private Reporter reporter = new Reporter();
        private CoverletGlobal coverletGlobal = new CoverletGlobal();

        public Generator(string outputDirectory, int fakeGenerationTime)
        {
            this.outputDirectory = outputDirectory;
            this.fakeGenerationTime = fakeGenerationTime;
        }

        public int FakeGenerationTime => fakeGenerationTime;

        public string Generate(List<string> dlls)
        {
            var coberturaFiles = coverletGlobal.RunCobertura(dlls, outputDirectory);
            var jsonPath = reporter.GenerateJsonReport(coberturaFiles, outputDirectory);
            return File.ReadAllText(jsonPath);
        }

        public string GenerateFakeData(string[] fakeNames)
        {
            var fake1 = new JsonAssemblyCoverage
            {
                name = FakeNames.Fake1,
                classesinassembly = new JsonClassCoverage[] {
                                new JsonClassCoverage {
                                    name = "Ns.Class1",
                                    branchcoverage = 0,
                                    coveredbranches = 0,
                                    totalbranches = 0,
                                    coverablelines = 2,
                                    coveredlines = 1,
                                    coverage = 1,
                                    totallines = 0,
                                    uncoveredlines = 0
                                },
                                new JsonClassCoverage {
                                    name = "Ns.Class2",
                                    branchcoverage = 0,
                                    coveredbranches = 0,
                                    totalbranches = 0,
                                    coverablelines = 2,
                                    coveredlines = 2,
                                    coverage = (decimal)0.8,
                                    totallines = 0,
                                    uncoveredlines = 0
                                },
                                new JsonClassCoverage
                                {
                                    name = "Ns2.Class1",
                                    branchcoverage = 0,
                                    coveredbranches = 0,
                                    totalbranches = 0,
                                    coverablelines = 4,
                                    coveredlines = 3,
                                    coverage = (decimal)0.6,
                                    totallines = 0,
                                    uncoveredlines = 0
                                },
                                new JsonClassCoverage
                                {
                                    name = "Ns2.Class2",
                                    branchcoverage = 0,
                                    coveredbranches = 0,
                                    totalbranches = 0,
                                    coverablelines = 5,
                                    coveredlines = 4,
                                    coverage = (decimal)0.4,
                                    totallines = 0,
                                    uncoveredlines = 0
                                }
                            },
                branchcoverage = 0,
                classes = 2,
                coverablelines = 0,
                coveredlines = 0,
                totallines = 0,
                coverage = 0,
                coveredbranches = 0,
                totalbranches = 0
            };
            var fake2 = new JsonAssemblyCoverage
            {
                name = FakeNames.Fake2,
                branchcoverage = 0,
                classes = 2,
                coverablelines = 0,
                coveredlines = 0,
                totallines = 0,
                coverage = 0,
                coveredbranches = 0,
                totalbranches = 0,
                classesinassembly = new JsonClassCoverage[]
                            {
                                new JsonClassCoverage
                                {
                                    name = "Ns.Class1",
                                    branchcoverage= null,
                                    coveredbranches= 0,
                                    totalbranches= 0,
                                    coverablelines= 6,
                                    coveredlines= 5,
                                    coverage= (decimal)0.33333333333333,
                                    totallines= 0,
                                    uncoveredlines= 0
                                },
                                new JsonClassCoverage
                                {
                                    name= "Ns.Class2",
                                    branchcoverage= (decimal)0.5,
                                    coveredbranches= 1,
                                    totalbranches= 2,
                                    coverablelines= 7,
                                    coveredlines= 6,
                                    coverage= (decimal) 0.2,
                                    totallines= 0,
                                    uncoveredlines= 0
                                },
                                new JsonClassCoverage
                                {
                                    name= "Ns2.Class1",
                                    branchcoverage= (decimal)0.333333,
                                    coveredbranches= 1,
                                    totalbranches= 3,
                                    coverablelines= 8,
                                    coveredlines= 7,
                                    coverage= (decimal)0.1,
                                    totallines= 0,
                                    uncoveredlines= 0
                                },
                                new JsonClassCoverage
                                {
                                    name= "Ns2.Class2",
                                    branchcoverage= 0,
                                    coveredbranches= 0,
                                    totalbranches= 0,
                                    coverablelines= 9,
                                    coveredlines= 8,
                                    coverage= 0,
                                    totallines= 0,
                                    uncoveredlines= 0
                                },
                                new JsonClassCoverage
                                {
                                    name= "Other",
                                    branchcoverage= 0,
                                    coveredbranches= 0,
                                    totalbranches= 0,
                                    coverablelines= 9,
                                    coveredlines= 8,
                                    coverage= (decimal)0.8888,
                                    totallines= 0,
                                    uncoveredlines= 0
                                }
                            }
            };

            var jsonSummaryResult = new JsonSummaryResult
            {
                summary = new JsonCoverageSummary
                {
                    assemblies = fakeNames.Length,
                    classes = 10,

                    branchcoverage = (decimal)0.5,
                    coveredbranches = 5,
                    totalbranches = 10,

                    coveredlines = 2,
                    coverablelines = 20,
                    uncoveredlines = 18,
                    linecoverage = (decimal)0.1,
                    totallines = 30,

                    files = 3,
                    parser = "Cobertura",
                    generatedon = new DateTime()
                },
                coverage = new JsonCoverageCoverage
                {
                    assemblies = new List<JsonAssemblyCoverage>
                    {
                        
                        
                    }
                }
            };
            
            if (fakeNames.Contains(FakeNames.Fake1))
            {
                jsonSummaryResult.coverage.assemblies.Add(fake1);
            }

            if (fakeNames.Contains(FakeNames.Fake2))
            {
                jsonSummaryResult.coverage.assemblies.Add(fake2);
            }

            return JsonConvert.SerializeObject(jsonSummaryResult);
        }
    }
}
