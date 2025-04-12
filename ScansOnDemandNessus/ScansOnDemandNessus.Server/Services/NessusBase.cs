using ScansOnDemandNessus.Server.Settings;
using ScansOnDemandNessus.Server.JsonMappers;
using ScansOnDemandNessus.Server.Utils;
namespace ScansOnDemandNessus.Server.Services
{
    public class NessusBase
    {
        protected HttpClient GetClient()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            var client =  new HttpClient(handler);

            client.DefaultRequestHeaders.Remove("X-ApiKeys");
            client.DefaultRequestHeaders.Add("X-ApiKeys",$"accessKey={AppSettings.AccessKey}; secretKey={AppSettings.SecretKey}");

            return client;
        }
    }
}
