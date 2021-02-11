using Microsoft.AspNetCore.Mvc;

namespace Micro.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("Hellow");
    }
}