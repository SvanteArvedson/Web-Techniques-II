using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weather.App.ViewModels
{
    public class AccountCreateViewModel
    {
        [Required(ErrorMessage = "Ange ett användarnamn")]
        [StringLength(256, ErrorMessage = "Användarnamnet får vara högt 256 tecken långt")]
        [Display(Name = "Användarnamn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ange ett lösenord")]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ange en epostadress")]
        [StringLength(256, ErrorMessage = "Epostadressen får vara högt 256 tecken lång")]
        [EmailAddress(ErrorMessage = "Ange en giltig epostadress")]
        [Display(Name = "Epost")]
        public string Email { get; set; }
    }
}