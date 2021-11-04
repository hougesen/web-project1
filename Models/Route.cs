using System;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class Route
    {
        public int RouteId { get; set; }
        public string RouteDescription { get; set; }
        public DateTime? RouteStartDate { get; set; }
        public DateTime? RouteEndDate { get; set; }
        public int? RouteStartLocationId { get; set; }
        public int? RouteEndLocationId { get; set; }
        public bool? RouteHighPriority { get; set; }
        public int? RouteStatusId { get; set; }
        public int? DriverId { get; set; }
        public int? DepartmentId { get; set; }
        public int? RouteEstTime { get; set; }

        public virtual Department Department { get; set; }
        public virtual User Driver { get; set; }
        public virtual Location RouteEndLocation { get; set; }
        public virtual Location RouteStartLocation { get; set; }
        public virtual RouteStatus RouteStatus { get; set; }
    }
}
