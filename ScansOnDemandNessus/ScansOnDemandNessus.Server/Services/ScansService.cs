using ScansOnDemandNessus.Server.JsonMappers;
using ScansOnDemandNessus.Server.Settings;
using ScansOnDemandNessus.Server.DTO;
using ScansOnDemandNessus.Server.Utils;
using ScansOnDemandNessus.Server.Models;
using System.Text.Json;
using System.Collections.Concurrent;
using System.Linq;

namespace ScansOnDemandNessus.Server.Services
{
    public class ScansService : NessusBase
    {
        private static ConcurrentBag<string> scheduledHosts = new ConcurrentBag<string>();
        DatabaseService _databaseService = new DatabaseService();

        public IEnumerable<Models.Scan> GetScans()
        {
            return GetClient()
                     .Get(AppSettings.AddressBase + "/scans")
                     .MapScanResponse();
        }

        public IEnumerable<string> GetPlugins()
        {
            return GetClient()
                     .Get(AppSettings.AddressBase + "/plugins/families")
                     .MapPluginsResponse();
        }

        public void CreateScan(ScanParameters scanParameters)
        {
            var body = new
            {
                uuid = AppSettings.TemplateUuid,
                settings = new
                {
                    name = $"scan - {scanParameters.HostName} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}",
                    enabled = "true",
                    text_targets = scanParameters.HostName,
                    launch = "ONETIME",
                    launch_now = true
                },
                credentials = new
                {
                    add = new
                    {
                        Host = new
                        {
                            Windows = new List<object>
                            {
                                new
                                {
                                    auth_method= "Password",
                                    username = scanParameters.Login,
                                    password = scanParameters.Password,
                                    domain = ""
                                }
                            }
                        }
                    },
                    edit = new object { },
                    delete = new List<object>()
                },
                plugins = JsonSerializer.Deserialize<Dictionary<string, object>>(scanParameters.Plugins)
            };

            GetClient().Post(AppSettings.AddressBase + "/scans", body);
        }

        public void CreateGroupedScan(ScanParameters scanParameters, List<string> hosts)
        {
            var body = new
            {
                uuid = AppSettings.TemplateUuid,
                settings = new
                {
                    name = $"scan - grouped {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}",
                    enabled = "true",
                    text_targets = string.Join(", ", hosts),
                    launch = "ONETIME",
                    launch_now = true
                },
                credentials = new
                {
                    add = new
                    {
                        Host = new
                        {
                            Windows = new List<object>
                            {
                                new
                                {
                                    auth_method= "Password",
                                    username = scanParameters.Login,
                                    password = scanParameters.Password,
                                    domain = ""
                                }
                            }
                        }
                    },
                    edit = new object { },
                    delete = new List<object>()
                },
                plugins = JsonSerializer.Deserialize<Dictionary<string, object>>(scanParameters.Plugins)
            };

            GetClient().Post(AppSettings.AddressBase + "/scans", body);
        }

        public NessusClientData_v2 DownloadScan(int id)
        {
            var body = new
            {
                format = "nessus"
            };

            string token = GetClient().Post(AppSettings.AddressBase + "/scans/" + id + "/export", body).MapExportResponse();

            string file = GetClient().Get(AppSettings.AddressBase + "/tokens/" + token + "/download");

            return file.MapResultResponse();
        }

        public void ScheduleScan(string hostName)
        {
            if(!scheduledHosts.Contains(hostName))
            {
                scheduledHosts.Add(hostName);
            }
        }

        public void RunScheduledScans()
        {
            var hosts = scheduledHosts.ToList();
            scheduledHosts.Clear();
            var allParameters = new List<ScanParameters>();

            foreach (var host in hosts)
            {
                var parmatersForThisHost = _databaseService.GetSettings(host);
                allParameters.Add(parmatersForThisHost);
            }

            var grouppedParameters = allParameters.GroupBy(x => x.Login + x.Password + x.Plugins);

            foreach (var group in grouppedParameters)
            {
                CreateGroupedScan(group.First(), group.Select(x => x.HostName).ToList());
            }
        }
    }
}
