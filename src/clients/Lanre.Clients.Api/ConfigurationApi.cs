
namespace Lanre.Clients.Api
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using FluentValidation.AspNetCore;
    using Lanre.Clients.Api.Models.Appointment;
    using System.Reflection;

    public static class ConfigurationApi
    {
        public static IServiceCollection ConfigureServicesApi(this IServiceCollection services)
        {
            return services
                .AddCustomApiVersion()
                .AddMvcCore()
                    .AddApplicationPart(typeof(Controllers.V1.AppointmentsController).Assembly) // Fix for integration tests
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
