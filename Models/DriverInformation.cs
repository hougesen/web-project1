using System;
using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class DriverInformation
    {
        public int DriverInformationId { get; set; }
        public int? UserId { get; set; }
        public int? LocationId { get; set; }
        public int? DriverLicenceId { get; set; }
        public int? LorryLicenceId { get; set; }
        public int? Eucertificate { get; set; }

        public virtual Licence DriverLicence { get; set; }
        public virtual Licence EucertificateNavigation { get; set; }
        public virtual Location Location { get; set; }
        public virtual Licence LorryLicence { get; set; }
        public virtual User User { get; set; }
    }
}
