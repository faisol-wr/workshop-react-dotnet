using Microsoft.AspNetCore.Mvc;

namespace BasicDotnetAPI.Controllers
{
    [ApiController]
    public class HomeController : Controller {
        [HttpGet]
        [Route("[action]")]
        public IActionResult Index() {
            return Ok(new {message = "Hello" });
        }
    }

}