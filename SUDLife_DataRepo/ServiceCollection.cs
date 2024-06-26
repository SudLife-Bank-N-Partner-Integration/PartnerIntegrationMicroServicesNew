using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SUDLife_DataRepo
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataAccess>(configuration);
            var connectionString = configuration.GetSection("ConnectionStrings:ConnectionStrings").Value;
            services.AddSingleton<IExectueProcdure>(sp => new DataAccess(connectionString));
            return services;
        }

        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataAccess>(configuration);
            var connectionString = configuration.GetSection("ConnectionStrings:ConnectionStrings").Value;
            services.AddSingleton<IExectueProcdure>(sp => new DataAccess(connectionString));
            return services;
        }

        public static IServiceCollection Data(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataAccess>(configuration);
            var connectionString = configuration.GetSection("ConnectionStrings:Default").Value;
            services.AddSingleton<IExectueProcdure>(sp => new DataAccess(connectionString));
            return services;
        }
    }
}


