namespace MVCProject.Models
{
    public class Bijlage
    {
        public int Id { get; set; }
        public int StudiebezoekId { get; set; } = default!;
        public string BestandsNaam { get; set; } = default!;

        //afgeleide properties
        public string Foldernaam => GetMapNaam(this.BestandsNaam);

        public string EchteBestandsNaam => GetBestandsNaam(this.BestandsNaam);

        //navigation properties
        public Studiebezoek Studiebezoek { get; set; } = default!;

        //methode
        private string GetMapNaam(string samengesteldeBestandsnaam)
        {
            if (samengesteldeBestandsnaam.StartsWith("stube"))
            {
                string[] delen = samengesteldeBestandsnaam.Split(new[] { "_split_" }, StringSplitOptions.None);
                var mapNaam = delen[0];
                return mapNaam;
            }

            return "";
        }

        private string GetBestandsNaam(string samengesteldeBestandsnaam)
        {
            if (samengesteldeBestandsnaam.StartsWith("stube"))
            {
                string[] delen = samengesteldeBestandsnaam.Split(new[] { "_split_" }, StringSplitOptions.None);
                var bestandsNaam = delen.Length > 1 ? delen[1] : "";
                return bestandsNaam;
            }

            return samengesteldeBestandsnaam;
        }
    }
}