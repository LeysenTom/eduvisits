using MVCProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels
{
    public class AfwezigheidViewModel
    {
        public int Id { get; set; }
        public string GebruikerId { get; set; } = default!; //DTR

        [Display(Name = "Startdatum"), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDatum { get; set; }

        [Display(Name = "Einddatum"), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EindDatum { get; set; }

        public Gebruiker? Gebruiker { get; set; }
    }
}