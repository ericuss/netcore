namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.Extensions.Logging;

    public static class LogExtension
    {
        public static IServiceCollection AddCustomLogger(this IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options
                    .AddConsole()
                    .AddDebug()
                    ;
            });

            return services;
        }
    }
}
