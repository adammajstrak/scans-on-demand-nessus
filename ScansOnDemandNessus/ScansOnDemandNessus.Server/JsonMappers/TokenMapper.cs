using ScansOnDemandNessus.Server.DTO;
using System.Text.Json;

namespace ScansOnDemandNessus.Server.JsonMappers
{
    public static class TokenMapper
    {
        public static string MapTokenResponse(this string jsonResponse)
        {
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(jsonResponse);

            var token = tokenResponse.Token;

            return token;
        }
    }
}
