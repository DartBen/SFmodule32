using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models;
using MvcStartApp.Models.DB;
using System.Diagnostics;

namespace MvcStartApp.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;

        // Также добавим инициализацию в конструктор
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Сделаем метод асинхронным
        public async Task<IActionResult> Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}