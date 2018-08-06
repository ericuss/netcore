namespace Lanre.Clients.Api
{
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
            return app.UseMvc(routes => routes.MapRoute("swagger", "{controller=Home}/{action=Swagger}"));
        }
    }
}
