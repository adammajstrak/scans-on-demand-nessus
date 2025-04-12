using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScansOnDemandNessus.Server.Settings;

namespace ScansOnDemandNessus.Server.Services
{
    public class ScanRunner : BackgroundService
    {
        ScansService _scansService = new ScansService();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if(AppSettings.RunScanEveryMinutes == 0)
            {
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(AppSettings.RunScanEveryMinutes), stoppingToken);
                _scansService.RunScheduledScans();
            }
        }
    }
}
