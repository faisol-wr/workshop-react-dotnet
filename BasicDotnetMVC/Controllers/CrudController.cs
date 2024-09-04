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

        public IActionResult Create()
        {
            return View();
        }

        public async Task CreateSave(BookModel bookModel)
        {
            var res = await client.PutAsJsonAsync(Api + "/api/Home/Create", bookModel);

            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("Success");
            }
            else
            {
                Response.Redirect("Error");
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}