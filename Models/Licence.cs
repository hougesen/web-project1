using System;
using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class Licence
    {
        public int LicenceId { get; set; }
        public int? LicenceTypeId { get; set; }
        public string LicenceImage { get; set; }
        public DateTime? LicenceExpirationDate { get; set; }

        public virtual LicenceType LicenceType { get; set; }
        public virtual DriverInformation DriverInformationDriverLicence { get; set; }
        public virtual DriverInformation DriverInformationEucertificateNavigation { get; set; }
        public virtual DriverInformation DriverInformationLorryLicence { get; set; }
    }
}
