using ScansOnDemandNessus.Server.DTO;
using System.Text.Json;

namespace ScansOnDemandNessus.Server.JsonMappers
{
    public static class ExportMapper
    {
        public static string MapExportResponse(this string jsonResponse)
        {
            var exportResponse = JsonSerializer.Deserialize<ExportResponse>(jsonResponse);

            var token = exportResponse.Token;

            return token;
        }
    }
}
