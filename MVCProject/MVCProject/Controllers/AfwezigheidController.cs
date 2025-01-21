using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.ViewModels;

namespace MVCProject.Controllers
{

    [Authorize(Roles = "Beheerder,Secretariaat")]
    public class AfwezigheidController : Controller
    {
        private readonly AzureDbContext _context;

        public AfwezigheidController(AzureDbContext context)
        {
            _context = context;
        }

        // GET: Afwezigheid
        public async Task<IActionResult> Index()
        {
            List<Afwezigheid> Afwezigen = await _context.Afwezigheden.Include(a => a.Gebruiker).ToListAsync();
            AfwezigheidIndexViewModel vm = new AfwezigheidIndexViewModel();
            vm.Afwezigheden = new List<AfwezigheidViewModel>();
            foreach (Afwezigheid item in Afwezigen)
            {
                AfwezigheidViewModel afmodel = new AfwezigheidViewModel();
                afmodel.Id = item.Id;
                afmodel.GebruikerId = item.GebruikerId;
                afmodel.StartDatum = item.StartDatum;
                afmodel.EindDatum = item.EindDatum;
                afmodel.Gebruiker = item.Gebruiker;
                vm.Afwezigheden.Add(afmodel);
            }

            return View(vm);
        }

        // GET: Afwezigheid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Afwezigheden == null)
            {
                return NotFound();
            }

            var afwezigheid = await _context.Afwezigheden
                .Include(a => a.Gebruiker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (afwezigheid == null)
            {
                return NotFound();
            }

            AfwezigheidViewModel model = new AfwezigheidViewModel()
            {
                Id = afwezigheid.Id,
                StartDatum = afwezigheid.StartDatum,
                EindDatum = afwezigheid.EindDatum,
                GebruikerId = afwezigheid.GebruikerId,
                Gebruiker = afwezigheid.Gebruiker,
            };

            return View(model);
        }

        // GET: Afwezigheid/Create
        public async Task<IActionResult> Create()
        {
            var gebruikers = await _context.Gebruikers.ToListAsync();

            if (gebruikers == null)
            {
                return NotFound();
            }
            //vm
            List<string> names = new List<string>();
            List<string> Ids = new List<string>();
            foreach (Gebruiker item in gebruikers)
            {
                names.Add(item.Voornaam + " " + item.Naam);
                Ids.Add(item.Id);
            }
            AfwezigheidEditViewModel vm = new AfwezigheidEditViewModel()
            {
                Id = 1,
                GebruikerId = "filler",
                GebruikerNaam = "filler",
                StartDatum = DateTime.Now,
                EindDatum = DateTime.Now,
                Gebruikers = names,
                GebruikersId = Ids
            };
            return View(vm);
        }

        // POST: Afwezigheid/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AfwezigheidEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Afwezigheid afwezigheid = new Afwezigheid();
                afwezigheid.GebruikerId = vm.GebruikerId;

                if (vm.StartDatum < vm.EindDatum)
                {
                    afwezigheid.StartDatum = vm.StartDatum;
                    afwezigheid.EindDatum = vm.EindDatum;
                }
                else
                {
                    afwezigheid.StartDatum = vm.EindDatum;
                    afwezigheid.EindDatum = vm.StartDatum;
                }
                _context.Add(afwezigheid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Afwezigheid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Afwezigheden == null)
            {
                return NotFound();
            }

            var afwezigheid = await _context.Afwezigheden.FindAsync(id);
            if (afwezigheid == null)
            {
                return NotFound();
            }
            AfwezigheidEditViewModel model = await ConvertToAfwezigheidEditVM(afwezigheid);
            return View(model);
        }

        // POST: Afwezigheid/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AfwezigheidEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("model is valid");
                Afwezigheid afwezigheid = new Afwezigheid() { EindDatum = vm.EindDatum, StartDatum = vm.StartDatum, GebruikerId = vm.GebruikerId, Id = vm.Id };
                if (afwezigheid.StartDatum > afwezigheid.EindDatum)
                {
                    DateTime start = afwezigheid.EindDatum;
                    afwezigheid.EindDatum = afwezigheid.StartDatum;
                    afwezigheid.StartDatum = start;
                }

                try
                {
                    _context.Update(afwezigheid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfwezigheidExists(afwezigheid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Afwezigheid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Afwezigheden == null)
            {
                return NotFound();
            }

            var afwezigheid = await _context.Afwezigheden
                .Include(a => a.Gebruiker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (afwezigheid == null)
            {
                return NotFound();
            }
            AfwezigheidViewModel model = new AfwezigheidViewModel()
            {
                Id = afwezigheid.Id,
                StartDatum = afwezigheid.StartDatum,
                EindDatum = afwezigheid.EindDatum,
                GebruikerId = afwezigheid.GebruikerId,
                Gebruiker = afwezigheid.Gebruiker,
            };

            return View(model);
        }

        // POST: Afwezigheid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Afwezigheden == null)
            {
                return Problem("Entity set 'AzureDbContext.Afwezigheden'  is null.");
            }
            var afwezigheid = await _context.Afwezigheden.FindAsync(id);
            if (afwezigheid != null)
            {
                _context.Afwezigheden.Remove(afwezigheid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AfwezigheidExists(int id)
        {
            return (_context.Afwezigheden?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<AfwezigheidEditViewModel> ConvertToAfwezigheidEditVM(Afwezigheid afwezigheid)
        {
            //vm
            List<string> names = new List<string>();
            List<string> Ids = new List<string>();
            var gebruikers = await _context.Gebruikers.ToListAsync();

            if (gebruikers == null)
            {
                gebruikers = new List<Gebruiker>();
            }

            foreach (Gebruiker item in gebruikers)
            {
                names.Add(item.Voornaam + " " + item.Naam);
                Ids.Add(item.Id);
            }

            AfwezigheidEditViewModel model = new AfwezigheidEditViewModel()
            {
                Id = afwezigheid.Id,
                GebruikerId = afwezigheid.Gebruiker.Id,
                GebruikerNaam = afwezigheid.Gebruiker.Voornaam + " " + afwezigheid.Gebruiker.Naam,
                StartDatum = afwezigheid.StartDatum,
                EindDatum = afwezigheid.EindDatum,
                Gebruikers = names,
                GebruikersId = Ids
            };
            return model;
        }
    }
}