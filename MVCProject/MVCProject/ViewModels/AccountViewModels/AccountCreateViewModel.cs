using Microsoft.AspNetCore.Identity;
using MVCProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCProject.Viewmodels.AccountViewModels
{
    public class AccountCreateViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = default!;

        [Required]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; } = default!;


        [Required]
        [Display(Name = "Achternaam")]
        public string Achternaam { get; set; } = default!;

        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string Gebruikersnaam { get; set; } = default!;

        public Boolean Leerkracht { get; set; }
        public Boolean SecretariaatsMedewerker { get; set; }
        public Boolean Coordinator { get; set; }
        public Boolean Directie { get; set; }


        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = default!;


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
