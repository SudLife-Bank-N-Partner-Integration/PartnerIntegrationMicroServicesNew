using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace SUDLife_Logger
{
    public class ClsLoggerConfigurator
    {
        public static void ConfigureLogger(IConfiguration configuration)
        {
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("EncryptedRequest", SqlDbType.NVarChar),
                    new SqlColumn("PlainRequest", SqlDbType.NVarChar),
                    new SqlColumn("PlainResponse", SqlDbType.NVarChar),
                    new SqlColumn("EncryptedResponse", SqlDbType.NVarChar)
                }
            };

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    tableName: "Logs",
                    autoCreateSqlTable: true,
                    columnOptions: columnOptions
                )
                .CreateLogger();
        }

        public static void CloseLogger()
        {
            Log.CloseAndFlush();
        }
    }
}
