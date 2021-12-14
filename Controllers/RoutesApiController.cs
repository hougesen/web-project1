using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AAOAdmin.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesAPIController : ControllerBase
    {
        private readonly AAOContext _context;

        public RoutesAPIController(AAOContext context)
        {
            _context = context;
        }

        // GET: api/routes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Route>>> GetRoutes()
        {
            return await _context.Routes.ToListAsync();
        }

        // GET: api/routes/5
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
        // PUT: api/routes/5
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

        // POST: api/routes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Route>> PostRoute(Route route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoute", new { id = route.RouteId }, route);
        }

        // DELETE: api/routes/5
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

        // GET: api/routes/calendar
        [HttpGet("calendar")]
        public async Task<IActionResult> RouteCalendar()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            var routes = await _context.Routes.Where(r => r.RouteStartDate >= firstDayOfMonth && r.RouteStartDate <= lastDayOfMonth).ToListAsync();
            int daysInMonth = Int32.Parse(lastDayOfMonth.ToShortDateString().Substring(0, 2));
            int[] calendar_dates = new int[daysInMonth];

            foreach (Route route in routes)
            {
                if (route.RouteStartDate != null)
                {
                    int date_index = Int32.Parse(route.RouteStartDate.ToString().Substring(0, 2));
                    if (date_index >= 0)
                    {
                        calendar_dates[date_index] += 1;
                    }
                }
            }

            return new JsonResult(calendar_dates);
        }
    }
}
