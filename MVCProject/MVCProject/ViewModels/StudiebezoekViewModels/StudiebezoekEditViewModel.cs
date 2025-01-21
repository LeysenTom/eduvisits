using Microsoft.AspNetCore.Mvc.Rendering;
using MVCProject.Models;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCProject.ViewModels.StudiebezoekViewModels
{
    public class StudiebezoekEditViewModel: IStudiebezoekViewModel
    {
        // Properties from Studiebezoek
        public int Id { get; set; }

        [Display(Name = "Aanvrager")]
        public string? AanvragerId { get; set; }
        [Display(Name = "Vak")]
        public int VakId { get; set; } //dtr

        [DataType(DataType.Date), Required(ErrorMessage = "Datum is verplicht"), Display(Name = "Datum Studiebezoek")]
        public DateTime Datum { get; set; }

        [DataType(DataType.Time), Display(Name = "Start Uur"), DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true), Required(ErrorMessage = "Star uur is verplicht")]
        public DateTime StartUur { get; set; }

        [DataType(DataType.Time), DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true), Display(Name = "Eindtijd"), Required(ErrorMessage = "Eind uur is verplicht")]
        public DateTime EindUur { get; set; }
        public string Reden { get; set; } = default!;
        [Display(Name = "Aantal Studenten")]
        public int? AantalStudenten { get; set; }

        [Display(Name = "Kostprijs")]
        public decimal KostprijsStudiebezoek { get; set; }
        [Display(Name = "Bus")]
        public bool VervoerBus { get; set; }
        [Display(Name = "Tram")]
        public bool VervoerTram { get; set; }
        [Display(Name = "Trein")]
        public bool VervoerTrein { get; set; }
        [Display(Name = "Te voet")]
        public bool VervoerTeVoet { get; set; }
        [Display(Name = "Auto")]
        public bool VervoerAuto { get; set; }
        [Display(Name = "Fiets")]
        public bool VervoerFiets { get; set; }

        [Display(Name = "Prijs Vervoer")]
        public decimal? KostprijsVervoer { get; set; }

        [Display(Name = "Afwezige studenten")]
        public string? AfwezigeStudenten { get; set; }
        public string? Opmerkingen { get; set; }

        [Display(Name = "Goedkeuring coördinator")]
        public bool? IsGoedgekeurdCo { get; set; }
        [Display(Name = "Goedkeuring directeur")]
        public bool? IsGoedgekeurdDir { get; set; }
        [Display(Name = "Afgewezen")]
        public bool? IsAfgewezen { get; set; }

        [Display(Name = "Opmerking afwijzing")]
        public string? OpmerkingAfgewezen { get; set; }
        [Display(Name = "Afgewerkt")]
        public bool? IsAfgewerkt { get; set; }

        public string? KlassenNamen { get; set; }

        public List<int> FilesToDelete { get; set; } = new List<int>();
        public List<IFormFile>? ExtraBijlages { get; set; }



        // Navigation properties
        public Gebruiker? Aanvrager { get; set; } 

        public Vak? Vak { get; set; }

        public List<Bijlage>? Bijlages { get; set; }
        public List<Gebruiker>? Begeleiders { get; set; }
        public List<FotoAlbum>? FotoAlbums { get; set; }
        public List<Klas>? Klassen { get; set; } 

        //voor de dropdown velden
        public SelectList? MogelijkOrganiserendeVakken { get; set; }

        public SelectList? MogelijkeDeelnemendeKlassen { get; set; }

        public SelectList? MogelijkeBegeleiders { get; set; } 

        public SelectList? IsGoedgekeurdCoSelectList { get; set; }
        public SelectList? IsGoedgekeurdDirSelectList { get; set; }
        public SelectList? IsAfgewezenSelectList { get; set; }
        public SelectList? IsAfgewerktSelectList { get; set; }
        //geselecteerde klassen en begeleiders

        [Display(Name = "Begeleiders")]
        public List<string>? GeselecteerdeBegeleiderIds { get; set; }
        [Display(Name = "Klassen")]
        public List<string>? GeselecteerdeKlasIds { get; set; }
        public StudiebezoekEditViewModel() { }
        // Constructor
        public void InitializeFromStudiebezoek(Studiebezoek studiebezoek)
        {
            Id = studiebezoek.Id;
            AanvragerId = studiebezoek.AanvragerId;
            VakId = studiebezoek.VakId;
            Datum = studiebezoek.Datum;
            StartUur = studiebezoek.StartUur;
            EindUur = studiebezoek.EindUur;
            Reden = studiebezoek.Reden;
            AantalStudenten = studiebezoek.AantalStudenten;
            KostprijsStudiebezoek = studiebezoek.KostprijsStudiebezoek;
            VervoerBus = TransformNullableBoolToBool(studiebezoek.VervoerBus);
            VervoerTram = TransformNullableBoolToBool(studiebezoek.VervoerTram);
            VervoerTrein = TransformNullableBoolToBool(studiebezoek.VervoerTrein);
            VervoerTeVoet = TransformNullableBoolToBool(studiebezoek.VervoerTeVoet);
            VervoerAuto = TransformNullableBoolToBool(studiebezoek.VervoerAuto);
            VervoerFiets = TransformNullableBoolToBool(studiebezoek.VervoerFiets);
            KostprijsVervoer = studiebezoek.KostprijsVervoer;
            AfwezigeStudenten = studiebezoek.AfwezigeStudenten;
            Opmerkingen = studiebezoek.Opmerkingen;
            IsGoedgekeurdCo = studiebezoek.IsGoedgekeurdCo;
            IsGoedgekeurdDir = studiebezoek.IsGoedgekeurdDir;
            IsAfgewezen = studiebezoek.IsAfgewezen;
            OpmerkingAfgewezen = studiebezoek.OpmerkingAfgewezen;
            IsAfgewerkt = studiebezoek.IsAfgewerkt;

            // Formatted or computed properties
            
            
           
            

            

            // Navigation properties
            Aanvrager = studiebezoek.Aanvrager;
            Vak = studiebezoek.Vak;
            Bijlages = studiebezoek.Bijlages;
            Begeleiders = studiebezoek.Begeleiders;
            FotoAlbums = studiebezoek.FotoAlbums;
            Klassen = studiebezoek.Klassen;
        }


        private bool TransformNullableBoolToBool(bool? boolean)
        {
            if (boolean == null)
            {
                return false;
            }
            else
            {
                return boolean.Value;
            }
        }
       

       

       

      

       
    }
}