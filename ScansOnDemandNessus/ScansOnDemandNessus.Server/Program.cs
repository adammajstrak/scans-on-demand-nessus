using Microsoft.Extensions.Hosting;
using ScansOnDemandNessus.Server.Services;
using ScansOnDemandNessus.Server.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddControllers();
builder.Services.AddHostedService<ScanRunner>();
GetConfig();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

static void GetConfig()
{
    var settingsFile = "appsettings.json";
#if DEBUG
    settingsFile = "appsettings.Development.json";
#endif

    IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(settingsFile).Build();
    AppSettings.AddressBase = configuration.GetSection("AppSettings:AddressBase").Value;
    AppSettings.User = configuration.GetSection("AppSettings:User").Value;
    AppSettings.Password = configuration.GetSection("AppSettings:Password").Value;
    AppSettings.TemplateUuid = configuration.GetSection("AppSettings:TemplateUuid").Value;
    AppSettings.DatabaseConnectionString = configuration.GetSection("AppSettings:DatabaseConnectionString").Value;
    AppSettings.AccessKey = configuration.GetSection("AppSettings:AccessKey").Value;
    AppSettings.SecretKey = configuration.GetSection("AppSettings:SecretKey").Value;
    AppSettings.RunScanEveryMinutes = int.Parse(configuration.GetSection("AppSettings:RunScanEveryMinutes").Value);
}
