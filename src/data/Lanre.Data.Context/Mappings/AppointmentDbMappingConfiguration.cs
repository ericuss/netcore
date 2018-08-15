
namespace Lanre.Data.Context.Mappings
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Core;
    using Data.Context.Contexts.Core;
    using Infrastructure.Entities;

    [ScheduleContextAttribute]
    public class AppointmentDbMappingConfiguration : EntityMappingConfiguration<Appointment>
    {
        public override void Map(EntityTypeBuilder<Appointment> entity)
        {
            entity.ToTable("Appointments");

            entity.HasKey(x => x.Id);
            entity.Property<DateTime>("EntryDate").HasDefaultValue(DateTime.Now);
        }

        public override void SeedData(EntityTypeBuilder<Appointment> entity, IHostingEnvironment env)
        {

            entity.HasData(
                new { Id = Guid.Parse("a09c1e14-d823-4281-926d-345299696beb"), EntryDate = DateTime.Now },
                new { Id = Guid.Parse("7fa0d8be-83b2-45fa-b3ce-e40b6195beaa"), EntryDate = DateTime.Now }
                );

            if (env.IsDevelopment())
            {
                entity.HasData(
                    new { Id = Guid.Parse("7dc436a5-29b9-475d-a394-f97a691f74f3"), EntryDate = DateTime.Now },
                    new { Id = Guid.Parse("e4fb1be6-4ba7-40c7-beef-e2b8c32751b6"), EntryDate = DateTime.Now },
                    new { Id = Guid.Parse("4da90981-6a19-48a1-a6d0-c1f413dc748d"), EntryDate = DateTime.Now },
                    new { Id = Guid.Parse("1849dfa8-9fac-4b0c-a63f-e3198935dc7b"), EntryDate = DateTime.Now },
                    new { Id = Guid.Parse("dc8fabd1-09b6-427f-8454-2066a70d2828"), EntryDate = DateTime.Now },
                    new { Id = Guid.Parse("c66f9fae-ea7c-43cf-b5bf-54f9164489f8"), EntryDate = DateTime.Now },
                    new { Id = Guid.Parse("f5950a6f-a698-4393-a9a5-5441cd03b1f7"), EntryDate = DateTime.Now }
                );
            }
        }
    }
}