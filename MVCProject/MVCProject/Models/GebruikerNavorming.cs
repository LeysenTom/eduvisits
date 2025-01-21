namespace MVCProject.Models
{
    public class GebruikerNavorming
    {
        public int Id { get; set; }
        public string DeelnemerId { get; set; } = default!; //DTR
        public int NavormingId { get; set; } //DTR

    }
}