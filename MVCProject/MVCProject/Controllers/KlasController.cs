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
using MVCProject.ViewModels.KlasViewModels;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "Beheerder,Coördinator")]
    public class KlasController : Controller
    {
        private readonly AzureDbContext _context;

        public KlasController(AzureDbContext context)
        {
            _context = context;
        }

        // GET: Klas
        public async Task<IActionResult> Index()
        {
            var klassen = await _context.Klassen.ToListAsync();

            if (_context.Klassen == null)
            {
                Problem("Entity set 'AzureDbContext.Klassen'  is null.");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                klassen = klassen.OrderBy(k => k.Naam).ToList();
                KlasIndexViewModel model = new KlasIndexViewModel();
                foreach (Klas item in klassen)
                {
                    KlasViewModel klasvm = new KlasViewModel() { Id = item.Id, Naam = item.Naam, Studiebezoeken = item.Studiebezoeken, Verwijderd = item.Verwijderd };
                    model.klassen.Add(klasvm);
                }
                return View(model);
            }
        }

        // GET: Klas/Details/5
        public IActionResult Details(int? id)
        {
            return RedirectToAction(nameof(Index));
            //if (id == null || _context.Klassen == null)
            //{
            //    return NotFound();
            //}

            //var klas = await _context.Klassen
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (klas == null)
            //{
            //    return NotFound();
            //}

            //return View(klas);
        }

        // GET: Klas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Naam,Verwijderd")] KlasViewModel klasvm)
        {
            if (!string.IsNullOrWhiteSpace(klasvm.Naam))
            {
                Klas klas = new Klas() { Verwijderd = klasvm.Verwijderd, Naam = klasvm.Naam };
                await _context.AddAsync<Klas>(klas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klasvm);
        }

        // GET: Klas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Klassen == null)
            {
                return NotFound();
            }

            var klas = await _context.Klassen.FindAsync(id);
            if (klas == null)
            {
                return NotFound();
            }
            KlasEditViewmodel klasEditViewmodel = new KlasEditViewmodel { Id = klas.Id, Naam = klas.Naam, Verwijderd = klas.Verwijderd };

            return View(klasEditViewmodel);
        }

        // POST: Klas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(KlasEditViewmodel vm)
        {
            //klas
            var klas = await _context.Klassen.FindAsync(vm.Id);

            if (klas == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                klas.Verwijderd = vm.Verwijderd;
                klas.Naam = vm.Naam;
                try
                {
                    _context.Update(klas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlasExists(klas.Id))
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

        // GET: Klas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Klassen == null)
            {
                return NotFound();
            }

            var klas = await _context.Klassen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klas == null)
            {
                return NotFound();
            }
            KlasViewModel model = new KlasViewModel() { Id = klas.Id, Naam = klas.Naam, Verwijderd = klas.Verwijderd, Studiebezoeken = klas.Studiebezoeken };
            return View(model);
        }

        // POST: Klas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klas = await _context.Klassen.FindAsync(id);

            if (klas == null)
            {
                return NotFound();
            }
            else
            {
                klas.Verwijderd = true;
                try
                {
                    _context.Update(klas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlasExists(klas.Id))
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

            //code om effectief te verwijderen
            //if (_context.Klassen == null)
            //{
            //    return Problem("Entity set 'AzureDbContext.Klassen'  is null.");
            //}
            //var klas = await _context.Klassen.FindAsync(id);

            ////klas studiebezoeken verwijderen
            //if (klas != null)
            //{
            //    List<KlasStudiebezoek> klasStudiebezoeken = _context.KlasStudiebezoeken.Where(k => k.KlasId == klas.Id).ToList();

            //    _context.KlasStudiebezoeken.RemoveRange(klasStudiebezoeken);
            //    _context.Klassen.Remove(klas);
            //}
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool KlasExists(int id)
        {
            return (_context.Klassen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}