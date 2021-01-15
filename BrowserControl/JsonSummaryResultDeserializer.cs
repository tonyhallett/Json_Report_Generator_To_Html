using BrowserControl.Deserialization;
using Newtonsoft.Json;
using System.IO;

namespace BrowserControl
{
    public static class JsonSummaryResultDeserializer
    {
        public static JsonSummaryResult DeserializeFromFile(string jsonPath)
        {
            var text = File.ReadAllText(jsonPath);
            var result = JsonConvert.DeserializeObject<JsonSummaryResult>(text);
            return result;
        }
    }
}
