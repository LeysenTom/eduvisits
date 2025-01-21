using NuGet.Common;

namespace MVCProject.Models
{
    public class Navorming
    {
        public int Id { get; set; }
        public string AanvragerId { get; set; } = default!; //in erd staat naamaanvragerid maar die naam klopt niet volgens mij. DTR
        public DateTime Datum { get; set; }
        public DateTime StartUur { get; set; }
        public DateTime EindUur { get; set; }
        public string Reden { get; set; } = default!;
        public decimal Kostprijs { get; set; }
        public bool? IsGoedGekeurdDir { get; set; }
        public bool? IsAfgewezen { get; set; }
        public string? OpmerkingAfgewezen { get; set; }
        public bool? IsAfgewerkt { get; set; }
        public string Status => GetStatus(IsGoedGekeurdDir);

        //navigation properties
        public Gebruiker Aanvrager { get; set; } = default!;

        public List<Gebruiker> Deelnemers { get; set; } = default!;

        private static string GetStatus(bool? IsGoedgekeurdDir)
        {
            if (IsGoedgekeurdDir == true)
                return "Goedgekeurd";
            if (IsGoedgekeurdDir == false)
                return "Afgekeurd";
            return "Niet beoordeeld";
        }
    }
}