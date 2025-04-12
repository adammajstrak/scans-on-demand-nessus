using System.Text.Json.Serialization;

namespace ScansOnDemandNessus.Server.DTO
{

    public class PluginsReponse
    {
        [JsonPropertyName("families")]
        public Family[] Families { get; set; }
    }

    public class Family
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
