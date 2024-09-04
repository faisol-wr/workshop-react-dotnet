using Microsoft.AspNetCore.Mvc;
using BasicDotnetMVC.Models;

namespace BasicDotnetMVC.Controllers
{
    public class CrudController : Controller
    {
        private string Api = "http://localhost:5037";
        private HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            ViewBag.books = await client.GetFromJsonAsync<List<BookModel>>(Api + "/api/Home/List");
            return View();
        }

    }
}