using AAOAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        // PUT: api/routes/5/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{routeId}/{userId}")]
        public async Task<IActionResult> PutRoute(int routeId, int userId)
        {
            var route = await _context.Routes.FindAsync(routeId);
            route.UserId = userId;
            // status: pending
            route.RouteStatusId = 2;
            _context.Entry(route).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(routeId))
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

            int[] calendar_dates = new int[lastDayOfMonth.Day];

            foreach (Route route in routes)
            {
                if (route.RouteStartDate != null)
                {
                    int date = route.RouteStartDate.GetValueOrDefault().Day;

                    if (date >= 0)
                    {
                        calendar_dates[date - 1] += 1;
                    }
                }
            }

            return new JsonResult(calendar_dates);
        }

        // GET: api/routes/request/1
        [HttpGet("request/{routeId}")]
        public async Task<IActionResult> DriveRequests(int routeId)
        {
            var requestsToDrive = await _context.SignUpDrivers.Include(s => s.User).Where(s => s.RouteId == routeId).Select(
                p => new DriveRequest()
                {
                    UserId = p.UserId,
                    RouteId = p.RouteId,
                    Name = p.User.UserFullName,
                    Email = p.User.UserEmail,
                    PhoneNumber = p.User.UserPhoneNumber
                }
            ).ToListAsync();

            return new JsonResult(requestsToDrive);
        }
    }
}
