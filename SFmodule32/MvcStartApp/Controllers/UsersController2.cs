using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models.DB;

namespace MvcStartApp.Controllers
{
    public class UsersController2 : Controller
    {
        // ссылка на репозиторий
        private readonly IBlogRepository _repo;

        public UsersController2(IBlogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }



    }
}
