using MVCProject.Models;

namespace MVCProject.Viewmodels.AccountViewModels
{
    public class AccountDetailsViewModel
    {
        public string Id { get; set; } = default!;
        public string Naam { get; set; } = default!;
        public string Voornaam { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Gebruikersnaam { get; set; } = default!;
        public string Rollen { get; set; } = default!;
    }
}
