
using Lanre.Data.Context.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lanre.Tests.Core
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Clients.Api;
    using Infrastructure.Cache;

    public class TestApiStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .ConfigureServicesApi()
                .AddDbContext<SchedulerContext>(x => x.UseInMemoryDatabase("Schedule"))
                .AddSingleton<ICustomMemoryCache, CustomMemoryCache>()
                ;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ConfigureApi();
        }
    }
}
