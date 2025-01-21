namespace MVCProject.ViewModels.StudiebezoekViewModels
{
    public interface IStudiebezoekViewModel
    {
        string Reden { get; set; }
        DateTime StartUur { get; set; }
        DateTime EindUur { get; set; }
        public int? AantalStudenten { get; set; }
        public int VakId { get; set; }
        public bool VervoerBus { get; set; }
        public bool VervoerTram { get; set; }
        public bool VervoerTrein { get; set; }
        public bool VervoerTeVoet { get; set; }
        public bool VervoerAuto { get; set; }
        public bool VervoerFiets { get; set; }
        public decimal? KostprijsVervoer { get; set; }

        public List<string>? GeselecteerdeBegeleiderIds { get; set; }

        public List<string>? GeselecteerdeKlasIds { get; set; }
    }     

    
}
