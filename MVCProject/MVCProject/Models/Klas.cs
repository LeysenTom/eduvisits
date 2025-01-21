﻿using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class Klas
    {
        public int Id { get; set; }
        public string Naam { get; set; } = default!;
        public bool Verwijderd { get; set; }

        //navigation properties
        public List<Studiebezoek> Studiebezoeken { get; set; } = default!;
    }
}