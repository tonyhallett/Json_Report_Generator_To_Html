using BrowserControl.Deserialization;
using System.Collections.Generic;

namespace BrowserControl
{
    public interface IGenerator
    {
        JsonSummaryResult Generate(List<string> dlls);
        JsonSummaryResult GenerateFakeData();

        int FakeGenerationTime { get; }
    }
}