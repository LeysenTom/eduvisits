using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCProject.Data;
using MVCProject.Data.UnitOfWork;
using MVCProject.Models;
using MVCProject.Viewmodels.AccountViewModels;
using System.Drawing;
using System.Text;
using System.Text.Encodings.Web;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "Beheerder,Directie")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Gebruiker> _signInManager;
        private readonly IUserStore<Gebruiker> _userStore;
        private readonly IUserEmailStore<Gebruiker> _emailStore;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<Gebruiker> userManager,
            IUserStore<Gebruiker> userStore,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Gebruiker> signInManager,
            IEmailSender emailSender,
            IUnitOfWork uow)
        {
            _userManager = userManager;
            _userStore = userStore;
            _roleManager = roleManager;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _emailSender = emailSender;
            _uow = uow;
        }

        [Authorize(Roles = "Beheerder")]
        public IActionResult Create()
        {
            AccountCreateViewModel vm = new AccountCreateViewModel();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, vm.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, vm.Email, CancellationToken.None);
                user.Voornaam = vm.Voornaam;
                user.Naam = vm.Achternaam;
                user.Gebruikersnaam = vm.Gebruikersnaam;
                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {

                    await SetRolesToUser(user, vm);

                    user.EmailConfirmed = true;
                    user.LockoutEnabled = true;

                    await _uow.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(vm);
        }

        public async Task<IActionResult> Index()
        {
            AccountViewModel vm = new AccountViewModel();
            var gebruikerslijst = await _uow.GebruikerRepository.GetAllAsync();
            vm.Gebruikers = gebruikerslijst.ToList();
            foreach(Gebruiker gebruiker in vm.Gebruikers)
            {
                var rollenLijst = await _userManager.GetRolesAsync(gebruiker);
                gebruiker.Rollen = rollenLijst.ToList();
            }
            return View(vm);
        }

        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> Details(string id)
        {
            var gebruiker = await _userManager.FindByIdAsync(id);

            if (gebruiker == null) return RedirectToAction("Index");

            var rollen = await _userManager.GetRolesAsync(gebruiker);
            var rollenString = "";
            for (int i = 0; i < rollen.Count(); i++)
            {
                rollenString += rollen[i];
                if (i < rollen.Count() - 1)
                    rollenString += ", ";
            }

            AccountDetailsViewModel vm = new AccountDetailsViewModel()
            {
                Id = id,
                Naam = gebruiker.Naam,
                Voornaam = gebruiker.Voornaam,
                Email = gebruiker.Email,
                Gebruikersnaam = gebruiker.Gebruikersnaam,
                Rollen = rollenString
            };

            return View(vm);
        }

        [Authorize(Roles = "Beheerder")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _uow.GebruikerRepository.GetByStringIdAsync(id);

            if (user != null)
            {
                user.Verwijderd = !user.Verwijderd;
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> Edit(string id)
        {
            var gebruiker = await _uow.GebruikerRepository.GetByStringIdAsync(id);
            if (gebruiker == null) { return NotFound(); }

            var rollen = await _userManager.GetRolesAsync(gebruiker);

            var leerkracht = rollen.Contains("Leerkracht");
            var secretariaat = rollen.Contains("Secretariaat");
            var coordinator = rollen.Contains("Coördinator");
            var directie = rollen.Contains("Directie");

            AccountEditViewModel vm = new AccountEditViewModel()
            {
                Id = id,
                Naam = gebruiker.Naam,
                Voornaam = gebruiker.Voornaam,
                Gebruikersnaam = gebruiker.Gebruikersnaam,
                Leerkracht = leerkracht,
                SecretariaatsMedewerker = secretariaat,
                Coordinator = coordinator,
                Directie = directie
            };
            return View(vm);
        }

        [Authorize(Roles = "Beheerder")]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AccountEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _uow.GebruikerRepository.GetByStringIdAsync(id);

                    if (user != null)
                    {
                        await DeleteUserRollen(user);

                        await SetRolesToUser(user, vm);

                        user.Naam = vm.Naam;
                        user.Voornaam = vm.Voornaam;
                        user.Gebruikersnaam = vm.Gebruikersnaam;

                        await _uow.SaveChangesAsync();
                    }

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_uow.GebruikerRepository.Search().Where(x => x.Id == id) != null)
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            return View(vm);
        }

        public async Task<IActionResult> FilterGebruikers(AccountViewModel vm)
        {
            var alleGebruikers = await _uow.GebruikerRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(vm.Zoekterm))
            {
                vm.Gebruikers = alleGebruikers.Where(g => g.VolledigeNaam.ToLower().Contains(vm.Zoekterm.ToLower())).ToList();
            }
            else
            {
                vm.Gebruikers = alleGebruikers.ToList();
            }

            foreach (Gebruiker gebruiker in vm.Gebruikers)
            {
                var rollenLijst = await _userManager.GetRolesAsync(gebruiker);
                gebruiker.Rollen = rollenLijst.ToList();
            }

            return View("Index", vm);
        }

        private async Task DeleteUserRollen(Gebruiker user)
        {
            var huidigeRollenlijst = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, huidigeRollenlijst);
        }
        private async Task SetRolesToUser(Gebruiker user, AccountEditViewModel vm)
        {
            var rolLijst = await _roleManager.Roles.ToListAsync();

            if (vm.Leerkracht)
            {
                var rol = rolLijst.Where(r => r.Name.Contains("Leerkracht")).ToList().FirstOrDefault();
                if (user != null && rol != null)
                    await _userManager.AddToRoleAsync(user, rol.Name);
            }
            if (vm.SecretariaatsMedewerker)
            {
                var rol = rolLijst.Where(r => r.Name.Contains("Secretariaat")).ToList().FirstOrDefault();
                if (user != null && rol != null)
                    await _userManager.AddToRoleAsync(user, rol.Name);
            }
            if (vm.Coordinator)
            {
                var rol = rolLijst.Where(r => r.Name.Contains("Coördinator")).ToList().FirstOrDefault();
                if (user != null && rol != null)
                    await _userManager.AddToRoleAsync(user, rol.Name);
            }
            if (vm.Directie)
            {
                var rol = rolLijst.Where(r => r.Name.Contains("Directie")).ToList().FirstOrDefault();
                if (user != null && rol != null)
                    await _userManager.AddToRoleAsync(user, rol.Name);
            }
        }
        private async Task SetRolesToUser(Gebruiker user, AccountCreateViewModel vm)
        {
            var rolLijst = await _roleManager.Roles.ToListAsync();


            if (vm.Leerkracht)
            {
                var rol = rolLijst.Where(r => r.Name.Contains("Leerkracht")).ToList().FirstOrDefault();
                if (user != null && rol != null)
                    await _userManager.AddToRoleAsync(user, rol.Name);
            }
            if (vm.SecretariaatsMedewerker)
            {
                var rol = rolLijst.Where(r => r.Name.Contains("Secretariaat")).ToList().FirstOrDefault();
                if (user != null && rol != null)
                    await _userManager.AddToRoleAsync(user, rol.Name);
            }
            if (vm.Coordinator)
            {
                var rol = rolLijst.Where(r => r.Name.Contains("Coördinator")).ToList().FirstOrDefault();
                if (user != null && rol != null)
                    await _userManager.AddToRoleAsync(user, rol.Name);
            }
            if (vm.Directie)
            {
                var rol = rolLijst.Where(r => r.Name.Contains("Directie")).ToList().FirstOrDefault();
                if (user != null && rol != null)
                    await _userManager.AddToRoleAsync(user, rol.Name);
            }
        }
        private Gebruiker CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Gebruiker>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Gebruiker)}'. " +
                    $"Ensure that '{nameof(Gebruiker)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<Gebruiker> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Gebruiker>)_userStore;
        }
    }
}