using Microsoft.AspNetCore.Mvc;

namespace Lanre.Clients.Api.Controllers.V1
{
    using System;
    using Infrastructure.Entities;
    using Core;
    using Models.Appointment;

    [ApiVersion("1.0", Deprecated = false)]
    //[ApiVersion("2.0")]
    //[Route("api/v{version:apiVersion}/[controller]")
    public class AppointmentController : ControllerCoreBasicApi<Appointment, AppointmentCreate>
    {
        [HttpGet("info")]
        [MapToApiVersion("1.0")]
        public IActionResult Info()
        {
            return this.Ok("information");
        }

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
