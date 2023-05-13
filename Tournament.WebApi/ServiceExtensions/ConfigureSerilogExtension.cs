using Serilog;

namespace Tournament.ServiceExtensions;

public static class ConfigureSerilogExtension
{
    public static void ConfigureSerilog(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        // Log.Logger = new LoggerConfiguration()
        //     .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
        //     .WriteTo.Console(LogEventLevel.Information,
        //         outputTemplate:
        //         "{Timestamp:HH:mm:ss:ms} LEVEL:[{Level}]| THREAD:|{ThreadId}| Source: |{Source}| {Message}{NewLine}{Exception}")
        //     .WriteTo.File("TournamentWebApiLog.txt", rollingInterval:
        //         RollingInterval.Day)
        //     .WriteTo.Seq("http://localhost:5341")
        //     .Destructure.UsingAttributes()
        //     .CreateLogger();
        // hostBuilder.UseSerilog();
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        hostBuilder.UseSerilog();
    }
}