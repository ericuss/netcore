
namespace Lanre.Clients.Api.Controllers.V1
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Data.Context.Contexts;
    using Infrastructure.Cache;
    using Infrastructure.Entities;
    using Core;
    using Models.Appointment;

    [ApiVersion("1.0", Deprecated = false)]
    //[ApiVersion("2.0")]
    //[Route("api/v{version:apiVersion}/[controller]")
    public class AppointmentsController : ControllerCoreBasicApi<AppointmentsController, Appointment, AppointmentCreate>
    {
        public AppointmentsController(ILogger<AppointmentsController> logger, ICustomMemoryCache cache, SchedulerContext context)
            : base(logger, cache, context)
        {
        }

        [HttpGet("info")]
        [MapToApiVersion("1.0")]
        public IActionResult Info()
        {
            _logger.LogInformation($"Get info");
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
