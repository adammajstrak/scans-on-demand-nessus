using ScansOnDemandNessus.Server.DTO;
using System.Text.Json;

namespace ScansOnDemandNessus.Server.JsonMappers
{
    public static class ScansMapper
    {
        public static IEnumerable<Models.Scan> MapScanResponse(this string jsonResponse)
        {
            var scansResponse = JsonSerializer.Deserialize<ScansResponse>(jsonResponse);
            
            var scansModel = scansResponse.Scans?.Select(x => new Models.Scan()
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            });

            return scansModel;
        }

        public static IEnumerable<string> MapPluginsResponse(this string jsonResponse)
        {
            var scansResponse = JsonSerializer.Deserialize<PluginsReponse>(jsonResponse);

            var plugins = scansResponse.Families?.Select(x => x.Name);

            return plugins;
        }
    }
}
