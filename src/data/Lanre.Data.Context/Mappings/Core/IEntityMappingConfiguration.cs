namespace Lanre.Data.Context.Mappings.Core
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder b, IHostingEnvironment currentEnvironment);
    }

    public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}