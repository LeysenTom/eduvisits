using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Services;
using MVCProject.ViewModels;

namespace MVCProject.Controllers
{
    public class NavormingenController : Controller
    {
        private readonly AzureDbContext _context;
        private readonly UserManager<Gebruiker> _userManager;

        public NavormingenController(
            AzureDbContext context,
            UserManager<Gebruiker> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Beheerder,Coördinator,Directie")]
        // GET: Navormingen
        public async Task<IActionResult> Index()
        {
            var azureDbContext = _context.Navormingen.Include(n => n.Aanvrager);
            return View(await azureDbContext.ToListAsync());
        }

        

            public async Task<IActionResult> PersoonlijkArchief()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var navormingen = await _context.Navormingen
                    .Include(n => n.Aanvrager)
                    .Where(n => n.AanvragerId == userId && n.IsAfgewerkt == true)
                    .ToListAsync();

                return View(navormingen);
            }
        

        // GET: Navormingen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Navormingen == null)
            {
                return NotFound();
            }

            var navorming = await _context.Navormingen
                .Include(n => n.Aanvrager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navorming == null)
            {
                return NotFound();
            }

            return View(navorming);
        }

        // GET: Navormingen/Create
        public IActionResult Create()
        {
            ViewData["AanvragerId"] = new SelectList(_context.Gebruikers, "Id", "Id");

            var gebruikersList = _context.Gebruikers.ToList();
            ViewBag.AanvragerId = new SelectList(gebruikersList, "Id", "VolledigeNaam");

            return View();
        }

        // POST: Navormingen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AanvragerId,Datum,StartUur,EindUur,Reden,Kostprijs,IsGoedGekeurdDir,IsAfgewezen,OpmerkingAfgewezen,IsAfgewerkt")] NavormingCreateViewModel vm)
        {

            if (ModelState.IsValid)
            {
                await _context.Navormingen.AddAsync(new Navorming()
                {
                    Id = vm.Id,
                    AanvragerId = vm.AanvragerId,
                    Datum = vm.Datum,
                    StartUur = vm.StartUur,
                    EindUur = vm.EindUur,
                    Reden = vm.Reden,
                    Kostprijs = vm.Kostprijs,
                    IsGoedGekeurdDir = vm.IsGoedGekeurdDir,
                    IsAfgewezen = vm.IsAfgewezen,
                    OpmerkingAfgewezen = vm.OpmerkingAfgewezen,
                    IsAfgewerkt = vm.IsAfgewerkt,
                });

                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewData["AanvragerId"] = new SelectList(_context.Gebruikers, "Id", "Id", vm.AanvragerId);
            return View(vm);
        }

        [Authorize(Roles = "Beheerder,Coördinator,Directie")]
        // GET: Navormingen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Navormingen == null)
            {
                return NotFound();
            }

            var navorming = await _context.Navormingen.FindAsync(id);
            if (navorming == null)
            {
                return NotFound();
            }
            ViewData["AanvragerId"] = new SelectList(_context.Gebruikers, "Id", "Id", navorming.AanvragerId);

            var gebruikersList = _context.Gebruikers.ToList();
            ViewBag.AanvragerId = new SelectList(gebruikersList, "Id", "VolledigeNaam");

            NavormingEditViewModel viewModel = new NavormingEditViewModel()
            {
                Id = navorming.Id,
                AanvragerId = navorming.AanvragerId,
                Datum = navorming.Datum,
                StartUur = navorming.StartUur,
                EindUur = navorming.EindUur,
                Reden = navorming.Reden,
                Kostprijs = navorming.Kostprijs,
                IsGoedGekeurdDir = navorming.IsGoedGekeurdDir,
                IsAfgewezen = navorming.IsAfgewezen,
                OpmerkingAfgewezen = navorming.OpmerkingAfgewezen,
                IsAfgewerkt = navorming.IsAfgewerkt,
            };
            return View(viewModel);
        }

        // POST: Navormingen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,AanvragerId,Datum,StartUur,EindUur,Reden,Kostprijs,IsGoedGekeurdDir,IsAfgewezen,OpmerkingAfgewezen,IsAfgewerkt")] NavormingEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Navorming navorming = new Navorming()
                    {
                        Id = vm.Id,
                        AanvragerId = vm.AanvragerId,
                        Datum = vm.Datum,
                        StartUur = vm.StartUur,
                        EindUur = vm.EindUur,
                        Reden = vm.Reden,
                        Kostprijs = vm.Kostprijs,
                        IsGoedGekeurdDir = vm.IsGoedGekeurdDir,
                        IsAfgewezen = vm.IsAfgewezen,
                        OpmerkingAfgewezen = vm.OpmerkingAfgewezen,
                        IsAfgewerkt = vm.IsAfgewerkt,
                    };
                    _context.Navormingen.Update(navorming);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavormingExists(vm.Id))
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
            ViewData["AanvragerId"] = new SelectList(_context.Gebruikers, "Id", "Id", vm.AanvragerId);
            return View(vm);
        }

        [Authorize(Roles = "Beheerder")]
        // GET: Navormingen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Navormingen == null)
            {
                return NotFound();
            }

            var navorming = await _context.Navormingen
                .Include(n => n.Aanvrager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navorming == null)
            {
                return NotFound();
            }

            return View(navorming);
        }

        // POST: Navormingen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Navormingen == null)
            {
                return Problem("Entity set 'AzureDbContext.Navormingen'  is null.");
            }
            var navorming = await _context.Navormingen.FindAsync(id);

            var gebruikernavormingen = _context.GebruikerNavormingen.Where(g => g.NavormingId == id);

            if (navorming != null)
            {
                _context.GebruikerNavormingen.RemoveRange(gebruikernavormingen);
                _context.Navormingen.Remove(navorming);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavormingExists(int id)
        {
            return (_context.Navormingen?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Authorize(Roles = "Directie")]
        [HttpPost]
        public async Task<IActionResult> Goedkeuren(int id)
        {
            var navorming = _context.Navormingen.Find(id);

            if (navorming == null)
            {
                return NotFound();
            }


            // Set IsGoedGekeurdDir to true
            navorming.IsGoedGekeurdDir = true;

            // Update the entity in the database
            _context.Navormingen.Update(navorming);
            _context.SaveChanges();

            //mail verzenden (zet recieverEmail op je eigen mail voor te testen)
            navorming.Aanvrager = await _userManager.FindByIdAsync(navorming.AanvragerId);

            string senderEmail = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result.Email;
            string senderName = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result.VolledigeNaam;
            string recieverEmail = navorming.Aanvrager.Email;
            //string recieverEmail = "test@example.com";
            string recieverName = navorming.Aanvrager.VolledigeNaam;
            string subject = $"Goedkeuring navorming '{navorming.Reden}'";
            string message = $"Beste {recieverName},\n\nje aanvraag voor een navorming met reden '{navorming.Reden}' op datum van {navorming.Datum} is goedgekeurd.";
            message += "\n\nMet vriendelijke groeten\n";
            message += senderName;

            EmailSender.SendEmail(senderEmail, senderName, recieverEmail, recieverName, subject, message);

            //afwezigheden aanmaken
            if (navorming.AanvragerId != null)
            {
                Afwezigheid afwezigheid = new Afwezigheid();
                afwezigheid.GebruikerId = navorming.AanvragerId;

                if (navorming.Datum.Add(navorming.StartUur.TimeOfDay) < navorming.Datum.Add(navorming.EindUur.TimeOfDay))
                {
                    afwezigheid.StartDatum = navorming.Datum.Add(navorming.StartUur.TimeOfDay);
                    afwezigheid.EindDatum = navorming.Datum.Add(navorming.EindUur.TimeOfDay);
                }
                else
                {
                    afwezigheid.StartDatum = navorming.Datum.Add(navorming.EindUur.TimeOfDay);
                    afwezigheid.EindDatum = navorming.Datum.Add(navorming.StartUur.TimeOfDay);
                }
                _context.Add(afwezigheid);
                _context.SaveChanges();
            }

            // Redirect to an appropriate action after approval
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Directie")]
        [HttpPost]
        public async Task<IActionResult> Afkeuren(int id, string afkeurReden)
        {
            var navorming = _context.Navormingen.Find(id);
            
            if (navorming == null)
            {
                return NotFound();
            }

            // Update OpmerkingAfgewezen with the entered reason
            navorming.OpmerkingAfgewezen = afkeurReden;

            // Set IsGoedGekeurdDir to false
            navorming.IsGoedGekeurdDir = false;
            navorming.IsAfgewezen = true;

            // Update the entity in the database
            _context.Navormingen.Update(navorming);
            _context.SaveChanges();

            //mail verzenden (zet recieverEmail op je eigen mail voor te testen)
            navorming.Aanvrager = await _userManager.FindByIdAsync(navorming.AanvragerId);

            string senderEmail = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result.Email;
            string senderName = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result.VolledigeNaam;
            string recieverEmail = navorming.Aanvrager.Email;
            //string recieverEmail = "test@example.com";
            string recieverName = navorming.Aanvrager.VolledigeNaam;
            string subject = $"Afkeuring navorming '{navorming.Reden}'";
            string message = $"Beste {recieverName},\n\ntot mijn grote spijt heb ik de navorming met reden '{navorming.Reden}' op datum van {navorming.Datum} moeten afkeuren omwille van volgende reden:\n\n";
            message += afkeurReden;
            message += "\n\nMet vriendelijke groeten\n";
            message += senderName;

            EmailSender.SendEmail(senderEmail, senderName, recieverEmail, recieverName, subject, message);

            // Redirect to an appropriate action after disapproval
            return RedirectToAction("Index","Home");
        }
    }
}