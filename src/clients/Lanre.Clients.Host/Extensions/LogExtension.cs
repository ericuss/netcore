
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    using Serilog;

    public static class LogExtension
    {
        public static IConfigurationBuilder LoadLoggerConfiguration(this IConfigurationBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel
                    .Verbose()
                    .WriteTo
                    .ColoredConsole()
                    .CreateLogger()
                ;

            return app;
        }

        public static IServiceCollection AddCustomLogger(this IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options
                    //.AddConsole()
                    //.AddDebug()
                    .AddSerilog()
                    ;
            });

            return services;
        }
    }
}
