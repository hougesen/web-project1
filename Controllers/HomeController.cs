using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AAOAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AAOContext _db;

        public HomeController(ILogger<HomeController> logger,
            AAOContext aaoContext
            )
        {
            _logger = logger;
            _db = aaoContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Drivers()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public User loginToUser(string email, string password)
        {
            User u1 = _db.Users.Where(myvar => myvar.UserEmail == "mads@mhouge.dk" ).FirstOrDefault<User>();

            return u1;
        }
    }
}

