using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
<<<<<<< HEAD
using System.Threading.Tasks;s
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System;
using System.Web;

=======
using System.Threading.Tasks;
>>>>>>> parent of 9be0cec (removed Ions code)

namespace AAOAdmin.Controllers
{
    public class UsersController : Controller
    {
        private readonly AAOContext _context;



      public UsersController(AAOContext context)
        {
            _context = context;
        }

        // GET: Users
       public async Task<IActionResult> Index()
        {
            var aAOContext = _context.Users.Where(u => u.UserTypeId == 2);
            return View(await aAOContext.ToListAsync());
        }

    public IActionResult test()
    {

      ViewBag.Message = "Welcome to my demo!";
      dynamic mymodel = new ExpandoObject();
      mymodel.Routes = GetRoutes();
      mymodel.DriversAvailable = GetDrivers();
      return View(mymodel);
    }

    public List<Route> GetRoutes()
      {
        List<Route> Routes = new List<Route>();
        return Routes;
      }

<<<<<<< HEAD
<<<<<<< HEAD
    public List<DriversAvailable> GetDrivers()
    {
      List<DriversAvailable> Drivers = new List<DriversAvailable>();
      return Drivers;
    }

    //using (AAOContext db = new AAOContext())
    /*{
      var routeList = _context.Routes
        .Include(r => r.Department)
          .Include(r => r.Driver)
          .Include(r => r.RouteEndLocation)
          .Include(r => r.RouteStartLocation)
          .Include(r => r.RouteStatus)
          .Include(d => d.Driver.DriverInformation)
          .Include(d => d.Driver.DriverInformation.User)
          .ToList();
      return View(routeList);
    }*/



=======
>>>>>>> parent of 9be0cec (removed Ions code)
=======
>>>>>>> parent of 9be0cec (removed Ions code)
    // GET: Users/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeId");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserTypeId,UserEmail,UserPassword,UserFullName,UserPhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeId", user.UserTypeId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeId", user.UserTypeId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserTypeId,UserEmail,UserPassword,UserFullName,UserPhoneNumber")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeId", user.UserTypeId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
