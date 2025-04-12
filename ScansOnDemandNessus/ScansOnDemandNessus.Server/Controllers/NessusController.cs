using Microsoft.AspNetCore.Mvc;
using ScansOnDemandNessus.Server.Models;
using ScansOnDemandNessus.Server.Services;
using System.Text.Json;

namespace ScansOnDemandNessus.Server.Controllers
{
    [ApiController]
    public class NessusController : ControllerBase
    {
        ScansService _scansService = new ScansService();
        DatabaseService _databaseService = new DatabaseService();

        [HttpGet("nessus/scans")]
        public IEnumerable<Models.Scan> GetAllScans()
        {
            var scans =  _scansService.GetScans();
            return scans;
        }

        [HttpPost("settings")]
        public void SaveSettings([FromBody] Models.ScanSettings scanSettings)
        {
            _databaseService.SaveSettings(scanSettings);
        }

        [HttpGet("settings")]
        public IEnumerable<string> GetHosts()
        {
            return _databaseService.GetHosts();
        }

        [HttpGet("nessus/plugins")]
        public IEnumerable<string> GetAllPlugins()
        {
            var plugins = _scansService.GetPlugins();
            return plugins;
        }

        [HttpPost("nessus/scans")]
        public void CreateScan([FromBody] Models.Host host)
        {
            var scanParameters = _databaseService.GetSettings(host.Name);
            _scansService.CreateScan(scanParameters);
        }

        [HttpPost("nessus/scans/schedule")]
        public void ScheduleScan([FromBody] Models.Host host)
        {
            _scansService.ScheduleScan(host.Name);
        }

        [HttpPost("nessus/scans/run")]
        public void RunScheduledScans()
        {
            _scansService.RunScheduledScans();
        }

        [HttpPost("nessus/scans/load")]
        public void LoadScan([FromBody]Models.Scan scan)
        {
            var result = _scansService.DownloadScan(scan.Id);
            _databaseService.InsertResult(result);
        }

        [HttpGet("nessus/results")]
        public IEnumerable<Models.Result> GetResults()
        {
            return _databaseService.GetResult();
        }

        [HttpDelete("nessus/results/{id}")]
        public void DeleteResult(int id)
        {
            _databaseService.DeleteResult(id);
        }
    }
}