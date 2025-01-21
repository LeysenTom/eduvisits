using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Data.UnitOfWork;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MVCProject.Models
{
    public class Gebruiker : IdentityUser
    {
        [PersonalData]
        public string Naam { get; set; } = default!;

        [PersonalData]
        public string Voornaam { get; set; } = default!;

        [PersonalData]
        public string Gebruikersnaam { get; set; } = default!;

        public string VolledigeNaam => this.Voornaam + " " + this.Naam;
        public string Initialen => GetInitials(Naam, Voornaam);
        public bool Verwijderd { get; set; }
        [NotMapped]
        public List<string> Rollen { get; set; }

        //navigation properties

        public List<Studiebezoek> StudiebezoekAanvragen { get; set; } = default!;
        public List<Studiebezoek> StudiebezoekBegeleidingen { get; set; } = default!;
        public List<Navorming> NavormingAanvragen { get; set; } = default!;
        public List<Navorming> NavormingDeelnames { get; set; } = default!;

        [JsonIgnore]
        public List<Afwezigheid> Afwezigheden { get; set; } = default!;

        //methode voor initialen

        private static string GetInitials(string naam, string voornaam)
        {
            string str = voornaam + " " + naam;
            string[] namen = str.Split(' ');
            string initialen = "";
            foreach (string s in namen)
            {
                initialen += s[0];
            }
            return initialen;
        }

        public Gebruiker()
        {
            Rollen = new List<string>();
        }
    }
}