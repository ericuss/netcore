using FluentValidation.AspNetCore;
using Lanre.Clients.Api.Models.Appointment;

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
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AppointmentCreateValidator>())
                .Services;
        }

        public static IApplicationBuilder ConfigureApi(this IApplicationBuilder app)
        {
            return app.UseMvc(routes => routes.MapRoute("swagger", "{controller=Home}/{action=Swagger}"));
        }
    }
}
