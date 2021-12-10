using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;


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
    public async Task<IActionResult> test()
    {
      var aAOContext = _context.Users.Where(u => u.UserTypeId == 2);
      return View(await aAOContext.ToListAsync());
    }
 
    // GET: Users
    /*public async Task<IActionResult> Index()
     {
         var aAOContext = _context.Users.Where(u => u.UserTypeId == 2);
         return View(await aAOContext.ToListAsync());
     }
    */
    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {
      ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
      ViewData["CurrentFilter"] = searchString;

      var list = from s in _context.DriversAvailables select s;

      list = list.Where(l=>l.DriversAvailableDate>=DateTime.Today).Include(u => u.User)
         .ThenInclude(u => u.DriverInformation)
         .ThenInclude(d => d.Location)
         .ThenInclude(l => l.City)
         .ThenInclude(c => c.Country);

      if (!String.IsNullOrEmpty(searchString))
      {
        var date = DateTime.Parse(searchString);
        list = list.Where(s => s.DriversAvailableDate==date
                               );
      }

      switch (sortOrder)
      {
        case "name_desc":
          list = list.OrderBy(s => s.User.UserFullName);
          break;
        case "Date":
          list = list.OrderBy(s => s.DriversAvailableDate);
          break;
        case "date_desc":
          list = list.OrderByDescending(s => s.DriversAvailableDate);

          //list = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<DriversAvailable, Country>)list.OrderByDescending(s => s.DriversAvailableDate);
          break;
        default:
        
          break;
      }
      return View(await list.AsNoTracking().ToListAsync());
    }
    public IActionResult test()
     {
      //using (AAOContext db = new AAOContext())
      {
        var list = _context.DriversAvailables
          .Include(u => u.User)
          .ThenInclude(u => u.DriverInformation)
          .ThenInclude(d => d.Location)
          .ThenInclude(l => l.City)
          .ThenInclude(c => c.Country)
          .Include(r => r.User.Routes)
          .ToList();

        ViewData["Routes"] = _context.Routes
          .Include(r => r.Department)
          .Include(r => r.User)
          .Include(r => r.RouteEndLocation)
          .Include(r => r.RouteStartLocation)
          .Include(r => r.RouteStatus).Where(s => s.RouteStatusId == 1)

          //.Include(r => r.RouteStartLocation)
          //.Include(r => r.RouteEndLocation)
          .ToList();
        return View(list);
      }
    }

    public   void testFunction ()
    {
      ViewData["Routes"] = _context.Routes
  .Include(r => r.Department)
  .Include(r => r.User)
  .Include(r => r.RouteEndLocation)
  .Include(r => r.RouteStartLocation)
  .Include(r => r.RouteStatus).Where(s => s.RouteStatusId == 1).ToList();
    }

    // Assigns a route to a driver
    public static int AssignRoute(int routeId, int userId)
    {
      //var _context = new AAOContext();
      //var route = _context.Routes.Where(r => r.RouteId == routeId).FirstOrDefault();
      //route.DriverId = userId;
      //route.RouteStatusId = 2;
      //_context.Update(route);
      //_context.SaveChanges();
      return routeId + userId;
    }


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
