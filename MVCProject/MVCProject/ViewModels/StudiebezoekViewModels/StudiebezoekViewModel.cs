using MVCProject.Models;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.StudiebezoekViewModels
{
    public class StudiebezoekViewModel
    {
        // Properties from Studiebezoek
        public int Id { get; set; }

        public string AanvragerId { get; set; }
        public int VakId { get; set; } //dtr
        public DateTime Datum { get; set; } //check requirements
        public DateTime StartUur { get; set; }
        public DateTime EindUur { get; set; }
        public string Reden { get; set; } = default!;
        [Display(Name = "Aantal Studenten")]
        public int? AantalStudenten { get; set; }
        [Display(Name = "Kostprijs Studiebezoek")]
        public decimal KostprijsStudiebezoek { get; set; }
        public bool? VervoerBus { get; set; }
        public bool? VervoerTram { get; set; }
        public bool? VervoerTrein { get; set; }
        public bool? VervoerTeVoet { get; set; }
        public bool? VervoerAuto { get; set; }
        public bool? VervoerFiets { get; set; }
        [Display(Name = "Kostprijs Vervoer")]
        public decimal? KostprijsVervoer { get; set; }
        [Display(Name = "Afwezige Studenten")]
        public string? AfwezigeStudenten { get; set; }
        public string? Opmerkingen { get; set; }
        public bool? IsGoedgekeurdCo { get; set; }
        public bool? IsGoedgekeurdDir { get; set; }
        public bool? IsAfgewezen { get; set; }
        [Display(Name = "Opmerking Afgewezen")]
        public string? OpmerkingAfgewezen { get; set; }
        [Display(Name = "Afgewerkt")]
        public bool? IsAfgewerkt { get; set; }

        public string? KlassenNamen { get; set; }

        // Formatted or computed properties
        public string FormattedDateTime { get; set; }

        public string? Vervoer { get; set; }

        public string? Status { get; set; }

        // Navigation properties
        public Gebruiker Aanvrager { get; set; } = default!;

        public Vak Vak { get; set; } = default!;

        public List<Bijlage> Bijlages { get; set; } = default!;
        public List<Gebruiker> Begeleiders { get; set; } = default!;
        public List<FotoAlbum> FotoAlbums { get; set; } = default!;
        public List<Klas> Klassen { get; set; } = default!;

        // Constructor
        public StudiebezoekViewModel(Studiebezoek studiebezoek)
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
            VervoerBus = studiebezoek.VervoerBus;
            VervoerTram = studiebezoek.VervoerTram;
            VervoerTrein = studiebezoek.VervoerTrein;
            VervoerTeVoet = studiebezoek.VervoerTeVoet;
            VervoerAuto = studiebezoek.VervoerAuto;
            VervoerFiets = studiebezoek.VervoerFiets;
            KostprijsVervoer = studiebezoek.KostprijsVervoer;
            AfwezigeStudenten = studiebezoek.AfwezigeStudenten;
            Opmerkingen = studiebezoek.Opmerkingen;
            IsGoedgekeurdCo = studiebezoek.IsGoedgekeurdCo;
            IsGoedgekeurdDir = studiebezoek.IsGoedgekeurdDir;
            IsAfgewezen = studiebezoek.IsAfgewezen;
            OpmerkingAfgewezen = studiebezoek.OpmerkingAfgewezen;
            IsAfgewerkt = studiebezoek.IsAfgewerkt;

            // Formatted or computed properties
            FormattedDateTime = GetFormattedDateTime(studiebezoek);
            Vervoer = GetVervoer(studiebezoek);
            KlassenNamen = GetKlassenNamen(studiebezoek);
            Status = GetStatus(studiebezoek);

            // Navigation properties
            Aanvrager = studiebezoek.Aanvrager;
            Vak = studiebezoek.Vak;
            Bijlages = studiebezoek.Bijlages;
            Begeleiders = studiebezoek.Begeleiders;
            FotoAlbums = studiebezoek.FotoAlbums;
            Klassen = studiebezoek.Klassen;
        }

        private string GetVervoer(Studiebezoek bezoek)
        {
            List<string> vervoerList = new List<string>();
            string vervoer = "";

            if (bezoek.VervoerBus == true)
            {
                vervoerList.Add("Bus");
            }
            if (bezoek.VervoerTram == true)
            {
                vervoerList.Add("Tram");
            }
            if (bezoek.VervoerTrein == true)
            {
                vervoerList.Add("Trein");
            }
            if (bezoek.VervoerTeVoet == true)
            {
                vervoerList.Add("Wandelend");
            }
            if (bezoek.VervoerAuto == true)
            {
                vervoerList.Add("Auto");
            }
            if (bezoek.VervoerFiets == true)
            {
                vervoerList.Add("Fiets");
            }

            if (vervoerList.Count > 0)
            {
                vervoer = string.Join(", ", vervoerList);
            }
            else
            {
                vervoer = "Geen";
            }
            return vervoer;
        }

        private string GetFormattedDateTime(Studiebezoek bezoek)
        {
            string formattedDateTime = $"{bezoek.Datum.ToShortDateString()} {bezoek.StartUur.ToString("HH:mm")}u - {bezoek.EindUur.ToString("HH:mm")}u";
            return formattedDateTime;
        }

        private string GetKlassenNamen(Studiebezoek bezoek)
        {
            List<string> klassenNamen = new List<string>();
            string klassen = "";
            foreach (var klas in bezoek.Klassen)
            {
                klassenNamen.Add(klas.Naam);
            }
            if (klassenNamen.Count > 0)
            {
                klassen = string.Join(", ", klassenNamen);
            }
            else
            {
                klassen = "Geen";
            }
            return klassen;
        }

        private string GetStatus(Studiebezoek bezoek)
        {
            string status = "";

            if (bezoek.IsAfgewezen == true)
                status = "Afgewezen!";


            if (bezoek.IsGoedgekeurdCo == null)
                status = "Nog te behandelen door coördinator en directeur.";

            if (bezoek.IsGoedgekeurdCo == true && bezoek.IsGoedgekeurdDir == null)
                status = "Nog te behandelen door directeur.";

            if (bezoek.IsAfgewezen == false)
                status = "Goedgekeurd!";

            return status;

        }
    }
}