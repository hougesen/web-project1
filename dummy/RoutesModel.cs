using System;

namespace AAOAdmin.Models
{
    public class RoutesModel
    {
        public int RouteId { get; set; }

        public string RouteDescription { get; set; }

        public DateTime RouteStartDate { get; set; }

        public DateTime RouteEndDate { get; set; }

        public int RouteStartLocationId { get; set; }
        public int RouteEndLocationId { get; set; }

        public bool RouteHighPriority { get; set; }

        public int RouteStatusId { get; set; }

        public int DriverId { get; set; }
    }
}
