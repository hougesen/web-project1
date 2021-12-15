namespace AAOAdmin.Controllers
{
    using AAOAdmin.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    public class DashboardController : Controller
    {

        private readonly AAOContext _db;

        public DashboardController(AAOContext aaoContext)
        {
            _db = aaoContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public static int[] CalendarDates()
        {
            var currentDate = DateTime.Now;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            AAOContext _context = new AAOContext();
            var routes = _context.Routes.Where(r => r.RouteStartDate >= firstDayOfMonth && r.RouteStartDate <= lastDayOfMonth);
            int daysInMonth = Int32.Parse(lastDayOfMonth.ToShortDateString().Substring(0, 2));
            int[] calendar_dates = new int[daysInMonth];

            foreach (Route route in routes)
            {
                if (route.RouteStartDate != null)
                {
                    int date_index = Int32.Parse(route.RouteStartDate.ToString().Substring(0, 2));
                    if (date_index >= 0)
                    {
                        calendar_dates[date_index - 1] += 1;
                    }
                }
            }

            return calendar_dates;
        }

        public static int GetRoutesMissingDrivers()
        {
            AAOContext _context = new AAOContext();
            int routes = _context.Routes.Where((r) => r.UserId == null && r.RouteStartDate >= DateTime.Now).Count();
            return routes;
        }

        public static int GetRoutesThisWeek()
        {
            DateTime currentDate = DateTime.Today;
            DateTime thisWeekStart = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            DateTime thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            AAOContext _context = new AAOContext();
            int routes = _context.Routes.Where(r => r.RouteStartDate >= thisWeekStart && r.RouteStartDate <= thisWeekEnd).Count();
            return routes;
        }

        public static int GetAvailableDriversThisWeek()
        {
            DateTime currentDate = DateTime.Today;
            DateTime thisWeekStart = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            DateTime thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            AAOContext _context = new AAOContext();
            int drivers = _context.DriversAvailables.Where(d => d.DriversAvailableDate >= thisWeekStart && d.DriversAvailableDate <= thisWeekEnd).Count();
            return drivers;
        }
    }
}


