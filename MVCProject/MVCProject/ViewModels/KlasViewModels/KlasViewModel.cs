using MVCProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.KlasViewModels
{
    public class KlasViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        public string Naam { get; set; } = default!;

        public bool Verwijderd { get; set; }

        //navigation properties
        public List<Studiebezoek> Studiebezoeken { get; set; } = default!;
    }
}