namespace Lanre.Clients.Api.Controllers.V1
{
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    public class ValuesController : ControllerCore
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(new[] { "asd", "ads" });
        }
    }
}
