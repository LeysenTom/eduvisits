using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class Afwezigheid
    {
        public int Id { get; set; }
        public string GebruikerId { get; set; } = default!; //DTR

        public DateTime StartDatum { get; set; }

        public DateTime EindDatum { get; set; }

        // navigation properties

        public Gebruiker Gebruiker { get; set; } = default!;
    }
}