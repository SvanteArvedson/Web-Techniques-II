using System.ComponentModel.DataAnnotations;

namespace Weather.App.ViewModels
{
    /// <summary>
    /// Properties for log in form
    /// </summary>
    public class AccountLoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}