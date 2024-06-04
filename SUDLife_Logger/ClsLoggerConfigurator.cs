using Microsoft.Extensions.Configuration;
using Serilog;


namespace SUDLife_Logger
{
    public static class ClsLoggerConfigurator
    {
        public static void ConfigureLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static void CloseLogger()
        {
            Log.CloseAndFlush();
        }
    }
}
