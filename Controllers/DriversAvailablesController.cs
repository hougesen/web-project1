using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AAOAdmin.Controllers
{
    public class DriversAvailablesController : Controller
    {
        private readonly AAOContext _context;

        public DriversAvailablesController(AAOContext context)
        {
            _context = context;
        }

        // GET: DriversAvailables
        public IActionResult Index()
        {
      using (AAOContext db=new AAOContext())
      {
        var list = db.Users.ToList();
        return View(list);
      }


      //   var aAOContext = _context.DriversAvailables.Include(d => d.User);
      /*return View(await _context.Database.ExecuteSqlRaw(@"SELECT Users.UserId, Users.UserFullName, 
Users.UserPhoneNumber, 
Users.UserEmail, 
DriversAvailable.DriversAvailableDate, 
CONCAT (Locations.LocationAddress, ', ', Locations.LocationPostalCode, ' ', Cities.CityName, ', ', Countries.CountryName) AS [Location]
FROM Users
INNER JOIN DriversAvailable
ON Users.UserId = DriversAvailable.UserId
LEFT JOIN DriverInformation
ON Users.UserId = DriverInformation.UserId
INNER JOIN Locations
ON DriverInformation.LocationId = Locations.LocationId
INNER JOIN Cities
ON Locations.CityId = Cities.CityId
INNER JOIN Countries
ON Cities.CountryId = Countries.CountryId;
").ToListAsync()); */
    }

        // GET: DriversAvailables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driversAvailable = await _context.DriversAvailables
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DriversAvailableId == id);
            if (driversAvailable == null)
            {
                return NotFound();
            }

            return View(driversAvailable);
        }

        // GET: DriversAvailables/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: DriversAvailables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriversAvailableId,UserId,DriversAvailableDate")] DriversAvailable driversAvailable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driversAvailable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", driversAvailable.UserId);
            return View(driversAvailable);
        }

        // GET: DriversAvailables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driversAvailable = await _context.DriversAvailables.FindAsync(id);
            if (driversAvailable == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", driversAvailable.UserId);
            return View(driversAvailable);
        }

        // POST: DriversAvailables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DriversAvailableId,UserId,DriversAvailableDate")] DriversAvailable driversAvailable)
        {
            if (id != driversAvailable.DriversAvailableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driversAvailable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriversAvailableExists(driversAvailable.DriversAvailableId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", driversAvailable.UserId);
            return View(driversAvailable);
        }

        // GET: DriversAvailables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driversAvailable = await _context.DriversAvailables
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DriversAvailableId == id);
            if (driversAvailable == null)
            {
                return NotFound();
            }

            return View(driversAvailable);
        }

        // POST: DriversAvailables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driversAvailable = await _context.DriversAvailables.FindAsync(id);
            _context.DriversAvailables.Remove(driversAvailable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriversAvailableExists(int id)
        {
            return _context.DriversAvailables.Any(e => e.DriversAvailableId == id);
        }
    }
}
