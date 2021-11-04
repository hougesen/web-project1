using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


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

        public IActionResult Sidenav()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                // DBEntity db = new DBEntity();
                var user = (from userlist in _db.Users
                            where userlist.UserEmail == login.UserEmail && userlist.UserPassword == login.UserPassword && userlist.UserId == 1
                            select new
                            {
                                userlist.UserId,
                                userlist.UserEmail
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {
                    /* Session["UserEmail"] = user.FirstOrDefault().UserEmail;
                     Session["UserID"] = user.FirstOrDefault().UserId;*/
                    return Redirect("/dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View(login);
        }

        public ActionResult WelcomePage()
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

