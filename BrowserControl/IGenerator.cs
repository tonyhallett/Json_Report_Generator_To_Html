using BrowserControl.Deserialization;
using System.Collections.Generic;

namespace BrowserControl
{
    public interface IGenerator
    {
        string Generate(List<string> dlls);
        string GenerateFakeData(string[] testProjectNames);

        int FakeGenerationTime { get; }
    }
}