using System;

namespace AAOAdmin.Models
{
    public class LicencesModel
    {
        public int LicenceId { get; set; }

        public int LicenceTypeId { get; set; }

        public string LicenceImage { get; set; }

        public DateTime LicenceExpirationDate { get; set; }
    }
}
