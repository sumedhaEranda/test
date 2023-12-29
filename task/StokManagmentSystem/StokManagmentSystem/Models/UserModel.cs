using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StokManagmentSystem.Models
{
    public class UserModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Username is required")]
       
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
       
        public string Password { get; set; }
    }
}