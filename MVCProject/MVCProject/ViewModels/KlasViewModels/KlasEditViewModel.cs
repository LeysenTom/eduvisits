using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCProject.ViewModels.KlasViewModels
{
    public class KlasEditViewmodel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        public string Naam { get; set; } = default!;

        public bool Verwijderd { get; set; }
    }
}