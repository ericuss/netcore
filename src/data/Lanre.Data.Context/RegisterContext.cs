
namespace Lanre.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Contexts;
    using Infrastructure.Entities.Configuration;

    public static class RegisterContext
    {
        public static void AddContext(this IServiceCollection services, Settings settings)
        {
            AddDB(services, settings);
        }

        private static void AddDB(IServiceCollection services, Settings settings)
        {
            services.AddDbContext<SchedulerContext>(o => o.UseSqlServer(settings.ConnectionStrings.Scheduler));
        }
    }
}
