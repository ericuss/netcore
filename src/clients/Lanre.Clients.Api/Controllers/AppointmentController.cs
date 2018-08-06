namespace Lanre.Clients.Api.Controllers
{
    using System;
    using Infrastructure.Entities;
    using Core;
    using Models.Appointment;

    public class AppointmentController : ControllerCoreBasicApi<Appointment, AppointmentCreate>
    {
        protected override Appointment CreateEntity(AppointmentCreate objectToCreate)
        {
            return new Appointment()
            {
                Id = Guid.NewGuid(),
                Description = objectToCreate.Description
            };
        }

        protected override void UpdateEntity(AppointmentCreate objectToUpdate, Appointment original)
        {
            original.Description = objectToUpdate.Description;
        }
    }
}
