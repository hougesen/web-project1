using System;
using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class DriversAvailable
    {
        public int DriversAvailableId { get; set; }
        public int UserId { get; set; }
        public DateTime DriversAvailableDate { get; set; }

        public virtual User User { get; set; }
    }
}
