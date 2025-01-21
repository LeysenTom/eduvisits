namespace MVCProject.Models
{
    public class Foto
    {
        public int Id { get; set; }
        public int FotoAlbumId { get; set; } //DTR
        public string NaamFoto { get; set; } = default!;
        public string Thumbnail { get; set; } = default!;

        //navigation properties
        public FotoAlbum FotoAlbum { get; set; } = default!;
    }
}