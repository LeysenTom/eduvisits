using MVCProject.Models;

namespace MVCProject.ViewModels.HomeViewModels
{
    public class DashboardViewModel
    {
        public List<Studiebezoek> Studiebezoeken { get; set; } = new List<Studiebezoek>();
        public List<Navorming> Navormingen { get; set; } = new List<Navorming>();
        public List<Studiebezoek> GoedTeKeurenStudiebezoeken { get; set; } = new List<Studiebezoek>();
        public List<Navorming> GoedTeKeurenNavormingen { get; set; } = new List<Navorming>();


    }
}
