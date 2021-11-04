using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class LicenceType
    {
        public LicenceType()
        {
            Licences = new HashSet<Licence>();
        }

        public int LicenceTypeId { get; set; }
        public string LicenceTypeName { get; set; }

        public virtual ICollection<Licence> Licences { get; set; }
    }
}
