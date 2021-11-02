using System;
using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class Location
    {
        public Location()
        {
            DriverInformations = new HashSet<DriverInformation>();
        }

        public int LocationId { get; set; }
        public string LocationAddress { get; set; }
        public string LocationPostalCode { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<DriverInformation> DriverInformations { get; set; }
    }
}
