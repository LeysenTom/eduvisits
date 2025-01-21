namespace MVCProject.Models
{
    public class KlasStudiebezoek
    {
        public int Id { get; set; }
        public int KlasId { get; set; } //dtr
        public int StudiebezoekId { get; set; } //dtr

        //navigation properties

        //public Klas Klas { get; set; } = default!;
        //public Studiebezoek Studiebezoek { get; set; } = default!;
    }
}