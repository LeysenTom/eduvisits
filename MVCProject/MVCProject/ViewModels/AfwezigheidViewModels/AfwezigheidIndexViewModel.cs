using MVCProject.Models;

namespace MVCProject.ViewModels
{
    public class AfwezigheidIndexViewModel
    {
        public List<AfwezigheidViewModel> Afwezigheden { get; set; }

        public AfwezigheidIndexViewModel()
        {
            Afwezigheden = new List<AfwezigheidViewModel>();
        }
    }
}