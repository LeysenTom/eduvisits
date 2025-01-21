using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCProject.ViewModels
{
    public class NavormingDetailViewmodel
    {
        public int Id { get; set; }
        public string AanvragerId { get; set; } = default!; //in erd staat naamaanvragerid maar die naam klopt niet volgens mij. DTR
        public DateTime Datum { get; set; }
        public DateTime StartUur { get; set; }
        public DateTime EindUur { get; set; }
        public string Reden { get; set; } = default!;
        public decimal Kostprijs { get; set; }
        [Display(Name = "Goedkeuring Directie")]
        public bool? IsGoedGekeurdDir { get; set; }
        [Display(Name = "Afgekeurd?")]
        public bool? IsAfgewezen { get; set; }
        [Display(Name = "Opmerking bij afkeuring")]
        public string? OpmerkingAfgewezen { get; set; }
        [Display(Name = "Navorming afgewerkt?")]
        public bool? IsAfgewerkt { get; set; }
    }
}
