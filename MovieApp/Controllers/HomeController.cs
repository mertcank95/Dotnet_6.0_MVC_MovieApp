using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Areas.Identity.Data;
using MovieApp.Models;
using System.Diagnostics;

namespace MovieApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger,UserManager<User> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            /* ViewData["UserID"] = _userManager.GetUserId(this.User);
              ViewData["Users"] = _userManager.Users.ToList();
              return View();*/
                var loggedInUserId = _userManager.GetUserId(User);
                var loggedInUser = _userManager.Users.FirstOrDefault(user => user.Id == loggedInUserId);

                if (loggedInUser != null)
                {
                    ViewData["LoggedInUser"] = loggedInUser;
                }
            

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