using Lanre.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Lanre.Clients.Api.Controllers.V1
{
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    public class ValuesController : ControllerCore
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Get entity", typeof(string))]
        public IActionResult Get()
        {
            return this.Ok(new[] { "asd", "ads" });
        }
    }
}
