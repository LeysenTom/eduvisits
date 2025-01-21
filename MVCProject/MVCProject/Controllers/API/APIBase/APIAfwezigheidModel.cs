namespace MVCProject.Controllers.API.APIBase
{
    public class APIAfwezigheidModel
    {
        public string GebruikerNaam { get; set; } = default!;
        public string GebruikerRol { get; set; } = default!;

        public DateTime StartDatum { get; set; }

        public DateTime EindDatum { get; set; }
    }
}