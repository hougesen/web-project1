using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class Department
    {
        public Department()
        {
            Routes = new HashSet<Route>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentContactNumber { get; set; }
        public string DepartmentEmail { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}
