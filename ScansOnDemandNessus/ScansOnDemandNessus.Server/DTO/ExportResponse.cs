using System.Text.Json.Serialization;

namespace ScansOnDemandNessus.Server.DTO
{

    public class ExportResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("file")]
        public int File { get; set; }
    }

}
