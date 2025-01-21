namespace MVCProject.Models
{
    public class FotoAlbum
    {
        public int Id { get; set; }
        public int StudiebezoekId { get; set; } //DTR
        public string Naam { get; set; } = default!;
        public DateTime Datum { get; set; }

        //navigation properties
        public Studiebezoek Studiebezoek { get; set; } = default!;

        public List<Foto> Fotos { get; set; } = default!;
    }
}