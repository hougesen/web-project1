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
            // Insert data 
            Country country = new Country() {
                CountryName="USA"
            };

            _db.Add(country);

            _db.SaveChanges();



            // Select data 
            User u1 = _db.Users.FirstOrDefault(myvar => myvar.UserId == 2);

            if (u1 == null)
            {
                throw new Exception("No user found");
            }


            u1.UserFullName = "John Mogensen";

            _db.Update(u1);

            _db.SaveChanges();

       



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


        public User loginToUser(string email, string password)
        {
            User u1 = _db.Users.Where(myvar => myvar.UserEmail == "mads@mhouge.dk" ).FirstOrDefault<User>();

            return u1;
        }
    }
}

