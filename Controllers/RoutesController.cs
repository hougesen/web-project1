using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace AAOAdmin.Controllers
{
    public class RoutesController : Controller
    {
        private readonly AAOContext _context;

        public RoutesController(AAOContext context)
        {
            _context = context;
        }

        // GET: Routes
        /*public async Task<IActionResult> Index()
        {
            var aAOContext = _context.Routes
            return View(await aAOContext.ToListAsync());
        }*/

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["StartDateSortParm"] = sortOrder == "SDate" ? "Sdate_desc" : "SDate";
            ViewData["EndDateSortParm"] = sortOrder == "EDate" ? "Edate_desc" : "EDate";
            ViewData["CurrentFilter"] = searchString;

            var route = from s in _context.Routes select s;
            route = route.Include(r => r.Department)
                         .Include(r => r.User)
                         .Include(r => r.RouteEndLocation)
                         .Include(r => r.RouteStartLocation)
                         .Include(r => r.RouteStatus);
            if (!String.IsNullOrEmpty(searchString))
            {
                var date = DateTime.Parse(searchString);
                var test = date.AddDays(1).AddSeconds(-1);
                /*var dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;*/
                route = route.Where(s => s.RouteStartDate <= test && s.RouteStartDate >= date);
            }
                switch (sortOrder)
                {
                    case "SDate":
                        route = route.OrderBy(s => s.RouteStartDate);
                        break;
                    case "Sdate_desc":
                        route = route.OrderByDescending(s => s.RouteStartDate);
                        break;
                    case "EDate":
                        route = route.OrderBy(s => s.RouteEndDate);
                        break;
                    case "Edate_desc":
                        route = route.OrderByDescending(s => s.RouteEndDate);
                        break;
                    default:

                        break;
                }
                return View(await route.AsNoTracking().ToListAsync());
            }
        








        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.Department)
                .Include(r => r.User)
                .Include(r => r.RouteEndLocation)
                .Include(r => r.RouteStartLocation)
                .Include(r => r.RouteStatus)
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewData["DriverId"] = new SelectList(_context.Users, "UserId", "UserName");
            ViewData["RouteEndLocationId"] = new SelectList(_context.Locations, "LocationId", "LocationAddress");
            ViewData["RouteStartLocationId"] = new SelectList(_context.Locations, "LocationId", "LocationAddress");
            ViewData["RouteStatusId"] = new SelectList(_context.RouteStatuses, "RouteStatusId", "RouteStatusName");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,RouteDescription,RouteStartDate,RouteEndDate,RouteStartLocationId,RouteEndLocationId,RouteHighPriority,RouteStatusId,DriverId,DepartmentId,RouteEstTime")] Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", route.DepartmentId);
            ViewData["DriverId"] = new SelectList(_context.Users, "UserId", "UserId", route.UserId);
            ViewData["RouteEndLocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", route.RouteEndLocationId);
            ViewData["RouteStartLocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", route.RouteStartLocationId);
            ViewData["RouteStatusId"] = new SelectList(_context.RouteStatuses, "RouteStatusId", "RouteStatusId", route.RouteStatusId);
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", route.DepartmentId);
            ViewData["DriverId"] = new SelectList(_context.Users, "UserId", "UserId", route.UserId);
            ViewData["RouteEndLocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", route.RouteEndLocationId);
            ViewData["RouteStartLocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", route.RouteStartLocationId);
            ViewData["RouteStatusId"] = new SelectList(_context.RouteStatuses, "RouteStatusId", "RouteStatusId", route.RouteStatusId);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteId,RouteDescription,RouteStartDate,RouteEndDate,RouteStartLocationId,RouteEndLocationId,RouteHighPriority,RouteStatusId,DriverId,DepartmentId,RouteEstTime")] Route route)
        {
            if (id != route.RouteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.RouteId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", route.DepartmentId);
            ViewData["DriverId"] = new SelectList(_context.Users, "UserId", "UserId", route.UserId);
            ViewData["RouteEndLocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", route.RouteEndLocationId);
            ViewData["RouteStartLocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", route.RouteStartLocationId);
            ViewData["RouteStatusId"] = new SelectList(_context.RouteStatuses, "RouteStatusId", "RouteStatusId", route.RouteStatusId);
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.Department)
                .Include(r => r.UserId)
                .Include(r => r.RouteEndLocation)
                .Include(r => r.RouteStartLocation)
                .Include(r => r.RouteStatus)
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.RouteId == id);
        }
    }
}
