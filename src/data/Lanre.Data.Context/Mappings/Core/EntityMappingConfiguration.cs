namespace Lanre.Data.Context.Mappings.Core
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public abstract class EntityMappingConfiguration<T> : IEntityMappingConfiguration<T> where T : class
    {
        public abstract void Map(EntityTypeBuilder<T> entity);

        public void Map(ModelBuilder b, IHostingEnvironment env)
        {
            var entity = b.Entity<T>();
            Map(entity);
            SeedData(entity, env);
        }

        public virtual void SeedData(EntityTypeBuilder<T> entity, IHostingEnvironment env)
        {
        }
    }
}