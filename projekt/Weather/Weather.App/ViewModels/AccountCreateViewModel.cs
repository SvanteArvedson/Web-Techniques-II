using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weather.App.ViewModels
{
    public class AccountCreateViewModel
    {
        [Required]
        [Display(Name = "Användarnamn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Epost")]
        public string Email { get; set; }
    }
}