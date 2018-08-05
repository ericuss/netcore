namespace Lanre.Clients.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]")]
    public class ValuesController : CoreController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(new[] { "asd", "ads" });
        }
    }
}
