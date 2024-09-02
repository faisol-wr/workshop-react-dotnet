using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BasicDotnetMVC.Models;

namespace BasicDotnetMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Hello()
    {
        ViewBag.message = "IActionResult Hello";
        return View();
    }

    public IActionResult GetParam(int x)
    {
        ViewBag.x = x + 10;
        return View();
    }

    public IActionResult List() {
        BookModel bookModel = new BookModel();
        bookModel.Id = 1;
        bookModel.Name = "C#.NET CORE";
        bookModel.Price = 1500;

        ViewBag.book = bookModel;
        return View();
    }

    public IActionResult ListFromModel() {
        ViewBag.books = new BookModel().List(15);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
