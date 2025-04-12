using System.Text.Json.Serialization;

namespace ScansOnDemandNessus.Server.Models
{
    public class ScanSettings
    {
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IDictionary<string, PluginStatus> Plugins { get; set; }
    }

    public class PluginStatus
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
