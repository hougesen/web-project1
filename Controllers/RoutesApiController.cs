using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAOAdmin.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RoutesAPIController : ControllerBase
  {
    private readonly AAOContext _context;

    public RoutesAPIController(AAOContext context)
    {
      _context = context;
    }

    // GET: api/RoutesAPI
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Route>>> GetRoutes()
    {
      return await _context.Routes.ToListAsync();
    }

    // GET: api/RoutesAPI/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Route>> GetRoute(int id)
    {
      var route = await _context.Routes.FindAsync(id);

      if (route == null)
      {
        return NotFound();
      }

      return route;
    }

    // Updates userid in routes table with onclick and saves in database
    // PUT: api/RoutesAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}/{userId}")]
    public async Task<IActionResult> PutRoute(int id, int userId)
    {
      var route = await _context.Routes.FindAsync(id);
      route.UserId = userId;
      route.RouteStatusId = 2;
      _context.Entry(route).State = EntityState.Modified;
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!RouteExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return new JsonResult(route);
    }

    // POST: api/RoutesAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Route>> PostRoute(Route route)
    {
      _context.Routes.Add(route);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetRoute", new { id = route.RouteId }, route);
    }

    // DELETE: api/RoutesAPI/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute(int id)
    {
      var route = await _context.Routes.FindAsync(id);
      if (route == null)
      {
        return NotFound();
      }

      _context.Routes.Remove(route);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool RouteExists(int id)
    {
      return _context.Routes.Any(e => e.RouteId == id);
    }
  }
}
