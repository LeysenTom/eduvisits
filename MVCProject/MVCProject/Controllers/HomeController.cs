using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data.UnitOfWork;
using MVCProject.Models;
using MVCProject.Services;
using MVCProject.ViewModels.HomeViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<Gebruiker> _signInManager;


        public HomeController(
            ILogger<HomeController> logger,
            UserManager<Gebruiker> userManager,
            IUnitOfWork uow,
            SignInManager<Gebruiker> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _uow = uow;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var user = await _uow.GetUserByEmail(vm.Email);

            if (user.Verwijderd)
            {
                ModelState.AddModelError(string.Empty, "Dit account is verwijderd");
            }
            else if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Ongeldige inlogpoging.");
                return View();
            }
            return View(vm);
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var studiebezoekenLijst = await _uow.StudiebezoekRepository.Search().Include(sb => sb.Aanvrager).ToListAsync();
            var navormingenLijst = await _uow.NavormingRepository.Search().Include(nv => nv.Aanvrager).ToListAsync();
            
            var aangevraagdeStudiebezoeken = new List<Studiebezoek>();
            var aangevraagdeNavormingen = new List<Navorming>();

            var goedTeKeurenStudiebezoeken = new List<Studiebezoek>();
            var goedTeKeurenNavormingen = new List<Navorming>();

            if (user != null)
            {
                aangevraagdeStudiebezoeken = studiebezoekenLijst.Where(sb => sb.AanvragerId == user.Id && sb.IsAfgewerkt != true && sb.IsAfgewezen != true).ToList();
                aangevraagdeNavormingen = navormingenLijst.Where(nv => nv.AanvragerId == user.Id && nv.IsAfgewerkt != true && nv.IsAfgewezen != true).ToList();
            }
            
            if (User.IsInRole("Directie"))
            {
                goedTeKeurenStudiebezoeken = studiebezoekenLijst.Where(sb => sb.IsGoedgekeurdCo == true && sb.IsGoedgekeurdDir == null).ToList();
                goedTeKeurenNavormingen = navormingenLijst.Where(nv => nv.IsGoedGekeurdDir == null).ToList();
            }

            if (User.IsInRole("Coördinator"))
            {
                goedTeKeurenStudiebezoeken = goedTeKeurenStudiebezoeken.Concat(studiebezoekenLijst.Where(sb => sb.IsGoedgekeurdCo == null).ToList()).ToList();
            }

            DashboardViewModel vm = new DashboardViewModel()
            {
                Studiebezoeken = aangevraagdeStudiebezoeken,
                Navormingen = aangevraagdeNavormingen,
                GoedTeKeurenNavormingen = goedTeKeurenNavormingen,
                GoedTeKeurenStudiebezoeken = goedTeKeurenStudiebezoeken
            };
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}