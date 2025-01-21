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
    [Authorize(Roles = "Beheerder,Coördinator")]
    public class VakkenController : Controller
    {
        private readonly AzureDbContext _context;

        public VakkenController(AzureDbContext context)
        {
            _context = context;
        }

        // GET: Vakken
        public async Task<IActionResult> Index()
        {
            return _context.Vakken != null ?
                        View(await _context.Vakken.ToListAsync()) :
                        Problem("Entity set 'AzureDbContext.Vakken'  is null.");
        }

        // GET: Vakken/Details/5
        public IActionResult Details(int? id)
        {
            return RedirectToAction(nameof(Index));
            /*if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);*/
        }

        // GET: Vakken/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vakken/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,Verwijderd")] VakCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _context.Vakken.AddAsync(new Vak()
                {
                    Id = vm.Id,
                    Naam = vm.Naam,
                    Verwijderd = vm.Verwijderd
                });

                await _context.SaveChangesAsync(); // Use asynchronous SaveChangesAsync
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Vakken/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken.FindAsync(id);
            if (vak == null)
            {
                return NotFound();
            }

            VakEditViewModel viewModel = new VakEditViewModel()
            {
                Id = vak.Id,
                Naam = vak.Naam,
                Verwijderd = vak.Verwijderd
            };

            return View(viewModel);
        }

        // POST: Vakken/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,Verwijderd")] VakEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Vak vak = new Vak()
                    {
                        Id = vm.Id,
                        Naam = vm.Naam,
                        Verwijderd = vm.Verwijderd
                    };
                    _context.Vakken.Update(vak);
                    await _context.SaveChangesAsync(); // Use asynchronous SaveChangesAsync
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Vakken.Find(id) != null)
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Vakken/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vakken == null)
            {
                return NotFound();
            }

            var vak = await _context.Vakken
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        // POST: Vakken/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vakken == null)
            {
                return Problem("Entity set 'AzureDbContext.Vakken'  is null.");
            }
            var vak = await _context.Vakken.FindAsync(id);
            if (vak != null)
            {
                vak.Verwijderd = true;
                _context.Vakken.Update(vak);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakExists(int id)
        {
            return (_context.Vakken?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}