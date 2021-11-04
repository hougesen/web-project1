﻿using System.Collections.Generic;

#nullable disable

namespace AAOAdmin.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
