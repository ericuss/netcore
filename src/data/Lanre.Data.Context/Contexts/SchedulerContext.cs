namespace Lanre.Data.Context.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Hosting;
    using Core;
    using Infrastructure.Entities;

    public class SchedulerContext : Context<SchedulerContext, ScheduleContextAttribute>
    {
        public SchedulerContext(DbContextOptions<SchedulerContext> options, IHostingEnvironment currentEnvironment) : base(options, currentEnvironment) { }

        public virtual DbSet<Appointment> Appointments { get; set; }
    }
}
