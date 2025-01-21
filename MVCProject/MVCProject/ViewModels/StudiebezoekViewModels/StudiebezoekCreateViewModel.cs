using Microsoft.AspNetCore.Mvc.Rendering;
using MVCProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCProject.ViewModels.StudiebezoekViewModels
{
    public class StudiebezoekCreateViewModel: IStudiebezoekViewModel
    {
        // Properties from Studiebezoek
        public int Id { get; set; }

        [Display(Name = "Aanvrager")]
        public string? AanvragerId { get; set; } = default!;

        [Required(ErrorMessage = "Vak is verplicht"), Display(Name = "Vak")]
        public int VakId { get; set; } //dtr

        [DataType(DataType.Date), Required(ErrorMessage = "Datum is verplicht"), Display(Name = "Datum Studiebezoek")]
        public DateTime Datum { get; set; } = DateTime.Now;

        [DataType(DataType.Time), Display(Name = "Starttijd"), DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true), Required(ErrorMessage = "Startijd is verplicht")]
        public DateTime StartUur { get; set; }

        [DataType(DataType.Time), DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true), Display(Name = "Eindtijd"), Required(ErrorMessage = "Eindtijd is verplicht")]
        public DateTime EindUur { get; set; }

        [Display(Name = "Reden Studiebezoek")]
        public string Reden { get; set; } = default!;

        [Display(Name = "Totaal aantal Studenten")]
        public int? AantalStudenten { get; set; }

        [Display(Name = "Kostprijs Studiebezoek")]
        public decimal KostprijsStudiebezoek { get; set; }

        [Display(Name = "Bus")]
        public bool VervoerBus { get; set; }

        [Display(Name = "Tram")]
        public bool VervoerTram { get; set; }

        [Display(Name = "Trein")]
        public bool VervoerTrein { get; set; }

        [Display(Name = "Te Voet")]
        public bool VervoerTeVoet { get; set; }

        [Display(Name = "Auto")]
        public bool VervoerAuto { get; set; }

        [Display(Name = "Fiets")]
        public bool VervoerFiets { get; set; }

        [Display(Name = "Kostprijs Vervoer")]
        public decimal? KostprijsVervoer { get; set; }

        public string? Opmerkingen { get; set; }

        public List<IFormFile>? Bijlages { get; set; } 


        // Navigation properties
        public Gebruiker? Aanvrager { get; set; }

        public Vak? Vak { get; set; }
       
        public List<Gebruiker> Begeleiders { get; set; } = new List<Gebruiker>();

        public List<Klas> Klassen { get; set; } = new List<Klas>();

        //voor de dropdown velden
        public SelectList? MogelijkOrganiserendeVakken { get; set; }

        public SelectList? MogelijkeDeelnemendeKlassen { get; set; }        

        public List<Gebruiker> MogelijkeBegeleiders { get; set; } = new List<Gebruiker>();

        //geselecteerde klassen en begeleiders
        public List<string>? GeselecteerdeBegeleiderIds { get; set; }

        public List<string>? GeselecteerdeKlasIds { get; set; } 

        // Constructor
        public StudiebezoekCreateViewModel()
        { }
    }
}