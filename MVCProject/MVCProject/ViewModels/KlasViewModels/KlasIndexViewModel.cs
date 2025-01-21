namespace MVCProject.ViewModels.KlasViewModels
{
    public class KlasIndexViewModel
    {
        public List<KlasViewModel> klassen { get; set; }

        public KlasIndexViewModel()
        {
            klassen = new List<KlasViewModel>();
        }
    }
}