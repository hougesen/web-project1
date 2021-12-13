using System;
using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class SignUpDriver
    {
        public int UserId { get; set; }
        public int RouteId { get; set; }

        public virtual Route Route { get; set; }
        public virtual User User { get; set; }
    }
}
