namespace Lanre.Data.Context.Contexts.Core
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Lanre.Data.Context.Mappings.Core;

    public class Context<TContext, TContextAttribute> : DbContext
        where TContext : DbContext
        where TContextAttribute : Attribute
    {
        private readonly IHostingEnvironment _currentEnvironment;

        public Context(DbContextOptions<TContext> options, IHostingEnvironment currentEnvironment)
            : base(options)
        {
            this._currentEnvironment = currentEnvironment;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var mappings = GetType().Assembly.DefinedTypes.Where(t =>
                typeof(IEntityMappingConfiguration).IsAssignableFrom(t)
                && t.IsDefined(typeof(TContextAttribute), false)
            );

            foreach (var type in mappings)
            {
                var builder = (IEntityMappingConfiguration)Activator.CreateInstance(type);
                builder.Map(modelBuilder, this._currentEnvironment);
            }
        }
    }
}
