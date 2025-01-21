namespace MVCProject.ViewModels
{
    public class NavormingCreateViewModel
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
    }
}
