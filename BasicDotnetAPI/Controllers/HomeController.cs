using Microsoft.AspNetCore.Mvc;

namespace BasicDotnetAPI.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            return Ok(new { message = "Hello" });
        }

        [HttpPost]
        [Route("[action]/{name}")]
        public IActionResult Index(string name)
        {
            return Ok(new { message = "Hello " + name });
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult MyPut() {
            return Ok(new {message = "my put"});
        }
    }

}