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

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.book = await client.GetFromJsonAsync<BookModel>(Api + "/api/Home/Info/" + id);
            return View();
        }

        public async Task EditSave(BookModel bookModel)
        {
            var res = await client.PostAsJsonAsync(Api + "/api/Home/Edit", bookModel);
            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("Index");
            }
            else
            {
                Response.Redirect("Error");
            }
        }

        public async Task Delete(int id)
        {
            var res = await client.DeleteAsync(Api + "/api/Home/Remove/" + id);
            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("/Crud/Index");
            }
            else
            {
                Response.Redirect("/Crud/Error");
            }
        }
    }
}