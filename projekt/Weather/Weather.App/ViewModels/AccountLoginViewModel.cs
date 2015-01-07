using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weather.App.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}