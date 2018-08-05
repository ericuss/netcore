namespace Lanre.Clients.Api
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;

    public static class ConfigurationApi
    {
        public static IServiceCollection ConfigureServicesApi(this IServiceCollection services)
        {
            return services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddApiExplorer()
                .Services;
        }

        public static IApplicationBuilder ConfigureApi(this IApplicationBuilder app)
        {
            return app.UseMvc();
            //.UseMvcCore(routes => routes.MapRoute("swagger", "{controller=DotNet}/{action=Swagger}"));
        }
    }
}
