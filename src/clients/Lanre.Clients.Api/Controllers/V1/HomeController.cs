namespace Lanre.Clients.Api.Controllers.V1
{
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    public class HomeController : ControllerCore
    {
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Swagger()
        {
            return Redirect("/swagger");
        }
    }
}
