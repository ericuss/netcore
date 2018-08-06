namespace Lanre.Clients.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
