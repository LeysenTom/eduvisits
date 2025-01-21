using MVCProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels
{
    public class AfwezigheidEditViewModel
    {
        public int Id { get; set; }
        public string GebruikerId { get; set; } = default!; //DTR
        public string GebruikerNaam { get; set; } = default!; //DTR

        [Display(Name = "Startdatum"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDatum { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Einddatum"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EindDatum { get; set; }

        public List<string> Gebruikers { get; set; }
        public List<string> GebruikersId { get; set; }

        public AfwezigheidEditViewModel()
        {
            Gebruikers = new List<string>();
            GebruikersId = new List<string>();
        }
    }
}