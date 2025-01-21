using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVCProject.Models
{
    public class Studiebezoek
    {
        public int Id { get; set; }
        public string AanvragerId { get; set; } = default!; //in erd staat naamaanvragerid maar die naam klopt niet volgens mij. DTR
        public int VakId { get; set; } //dtr
        public DateTime Datum { get; set; } //check requirements
        public DateTime StartUur { get; set; }
        public DateTime EindUur { get; set; }
        public string Reden { get; set; } = default!;
        public int? AantalStudenten { get; set; }
        public decimal KostprijsStudiebezoek { get; set; }
        public bool? VervoerBus { get; set; }
        public bool? VervoerTram { get; set; }
        public bool? VervoerTrein { get; set; }
        public bool? VervoerTeVoet { get; set; }
        public bool? VervoerAuto { get; set; }
        public bool? VervoerFiets { get; set; }
        public decimal? KostprijsVervoer { get; set; }
        public string? AfwezigeStudenten { get; set; }
        public string? Opmerkingen { get; set; }
        public bool? IsGoedgekeurdCo { get; set; }
        public bool? IsGoedgekeurdDir { get; set; }
        public bool? IsAfgewezen { get; set; }
        public string? OpmerkingAfgewezen { get; set; }
        public bool? IsAfgewerkt { get; set; }
        public string Status => GetStatus(IsGoedgekeurdDir, IsGoedgekeurdCo);

        //navigation properties

        public Gebruiker Aanvrager { get; set; } = default!;
        public Vak Vak { get; set; } = default!;

        public List<Bijlage> Bijlages { get; set; } = default!;
        public List<Gebruiker> Begeleiders { get; set; } = default!;
        public List<FotoAlbum> FotoAlbums { get; set; } = default!;
        public List<Klas> Klassen { get; set; } = default!;

        private static string GetStatus(bool? IsGoedgekeurdDir, bool? IsGoedgekeurdCo)
        {
            if (IsGoedgekeurdCo == true && IsGoedgekeurdDir == true)
                return "Goedgekeurd";
            if (IsGoedgekeurdCo == false || IsGoedgekeurdDir == false)
                return "Afgekeurd";
            return "Niet beoordeeld";
        }
    }
}