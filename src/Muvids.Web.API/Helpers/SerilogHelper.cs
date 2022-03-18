using Serilog;

namespace Muvids.Web.API.Helpers;

/// <summary>
///Adding Serilog https://blog.datalust.co/using-serilog-in-net-6/
// https://www.claudiobernasconi.ch/2022/01/28/how-to-use-serilog-in-asp-net-core-web-api/
/// </summary>
public static class SerilogHelper
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
                  .ReadFrom.Configuration(builder.Configuration)
                  .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                  .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
    }


}
