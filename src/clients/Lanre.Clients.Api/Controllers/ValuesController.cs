namespace Lanre.Clients.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    public class ValuesController : ControllerCore
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(new[] { "asd", "ads" });
        }
    }
}
