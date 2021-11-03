using System;
using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class RouteStatus
    {
        public RouteStatus()
        {
            Routes = new HashSet<Route>();
        }

        public int RouteStatusId { get; set; }
        public string RouteStatusName { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}
