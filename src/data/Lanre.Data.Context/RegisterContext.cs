
namespace Lanre.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Contexts;
    using Infrastructure.Entities.Configuration;

    public static class RegisterContext
    {
        public static IServiceCollection AddContext(this IServiceCollection services, Settings settings)
        {
            AddDB(services, settings);
            return services;
        }

        private static void AddDB(IServiceCollection services, Settings settings)
        {
            services.AddDbContext<SchedulerContext>(o => o.UseSqlServer(settings.ConnectionStrings.Scheduler));
        }
    }
}
