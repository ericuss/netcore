namespace Lanre.Clients.Api.Tests
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class TestApiStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureServicesApi();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ConfigureApi();
        }
    }
}
