using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AAOAdmin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter email.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [StringLength(255)]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Adgangskode")]
        [StringLength(255)]
        public string UserPassword { get; set; }

    }

    [Table("Users")]
    public class Users
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
