using Microsoft.AspNetCore.Identity;
using MVCProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCProject.Viewmodels.AccountViewModels
{
    public class AccountEditViewModel
    {
        [Required]
        public string Id { get; set; } = default!;
        [Required]
        public string Naam { get; set; } = default!;
        [Required]
        public string Voornaam { get; set; } = default!;
        public string Gebruikersnaam { get; set; } = default!;
        public Boolean Leerkracht { get; set; }
        public Boolean SecretariaatsMedewerker { get; set; }
        public Boolean Coordinator { get; set; }
        public Boolean Directie { get; set; }
        public Boolean Beheerder { get; set; }
    }
}
