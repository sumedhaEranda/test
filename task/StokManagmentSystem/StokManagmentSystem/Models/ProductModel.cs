using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StokManagmentSystem.Models
{
    public class ProductModel
    {
        public int productcode { get; set; }

        [Required(ErrorMessage = "Username is required")]

        public string ProductName { get; set; }

    }
}