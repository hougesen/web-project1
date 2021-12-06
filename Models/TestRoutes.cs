using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAOAdmin.Models
{
  public class TestRoutes
  {
    public partial class DriversAvailable
    {
      public int DriversAvailableId { get; set; }
      public int UserId { get; set; }
      public DateTime DriversAvailableDate { get; set; }

      public virtual User User { get; set; }

    }
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
      public virtual ICollection<SignUpDriver> SignUpDrivers { get; set; }
    }
  }


}
