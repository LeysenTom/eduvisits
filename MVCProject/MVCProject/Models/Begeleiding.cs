using System.Diagnostics.Contracts;

namespace MVCProject.Models
{
    public class Begeleiding
    {
        public int Id { get; set; }
        public int StudiebezoekId { get; set; }
        public string GebruikerId { get; set; } = default!;
    }
}