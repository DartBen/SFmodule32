using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models.DB;

namespace MvcStartApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBlogRepository _repo;

        public UsersController(IBlogRepository repo)
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
        //[HttpPost]
        //public async Task<IActionResult> RegisterUser(string FirstName, string LastName)
        //{
        //    var newUser = new User()
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = FirstName,
        //        LastName = LastName,
        //        JoinDate = DateTime.Now
        //    };

        //    // Добавим в базу
        //    await _repo.AddUser(newUser);

        //    // Выведем результат
        //    Console.WriteLine($"User with id {newUser.Id}, named {newUser.FirstName} was successfully added on {newUser.JoinDate}");
        //    return View("Register");
        //}
        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            await _repo.AddUser(newUser);
            return View(newUser);
        }
    }
}
