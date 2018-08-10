namespace Microsoft.Extensions.DependencyInjection
{
    using AspNetCore.Builder;

    public static class ProfilerExtension
    {
        public static IServiceCollection AddCustomProfiler(this IServiceCollection services)
        {
            services.AddMiniProfiler(options =>
                options.RouteBasePath = "/profiler"
            );

            return services;
        }

        public static IApplicationBuilder UseCustomProfiler(this IApplicationBuilder app)
        {
            return app.UseMiniProfiler();
        }
    }
}
