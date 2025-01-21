using MVCProject.Models;

namespace MVCProject.Viewmodels.AccountViewModels
{
    public class AccountViewModel
    {
        public List<Gebruiker> Gebruikers { get; set; } = default!;
        public string Zoekterm { get; set; } = default!;
    }
}
