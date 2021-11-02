using System;
using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int? UserTypeId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserPhoneNumber { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual DriverInformation DriverInformation { get; set; }
    }
}
