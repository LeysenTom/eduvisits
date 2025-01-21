using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Data.UnitOfWork;
using MVCProject.Models;
using MVCProject.Services;
using MVCProject.ViewModels.StudiebezoekViewModels;
using NuGet.Protocol.Plugins;

namespace MVCProject.Controllers
{

    public class StudiebezoekenController : Controller
    {
        private readonly AzureDbContext _context;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;





        public StudiebezoekenController(IWebHostEnvironment hostingEnvironment, AzureDbContext context, UserManager<Gebruiker> userManager)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;

        }

        // GET: Studiebezoeken
        //public async Task<IActionResult> Index()
        //{
        //    var azureDbContext = _context.Studiebezoeken.Include(s => s.Aanvrager).Include(s => s.Vak);
        //    return View(await azureDbContext.ToListAsync());
        //}
        [Authorize(Roles = "Beheerder,Coördinator,Directie")]
        public async Task<IActionResult> Index()
        {
            var studiebezoeken = await _context.Studiebezoeken
                .Include(s => s.Aanvrager)
                .Include(s => s.Vak)
                .Include(s => s.Bijlages)
                .Include(s => s.Begeleiders)
                .Include(s => s.FotoAlbums)
                .Include(s => s.Klassen)
                .OrderByDescending(sb => sb.Datum)
                .Select(sb => new StudiebezoekViewModel(sb))
                .ToListAsync();

            return View(studiebezoeken);
        }

        public async Task<IActionResult> PersoonlijkArchief()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studiebezoeken = await _context.Studiebezoeken
                .Include(s => s.Aanvrager)
                .Include(s => s.Vak)
                .Include(s => s.Bijlages)
                .Include(s => s.Begeleiders)
                .Include(s => s.FotoAlbums)
                .Include(s => s.Klassen)
                .Where(s => s.AanvragerId == userId && s.IsAfgewerkt == true)
                .OrderByDescending(sb => sb.Datum)
                .Select(sb => new StudiebezoekViewModel(sb))
                .ToListAsync();

            return View(studiebezoeken);
        }


        [Authorize(Roles = "Secretariaat,Coördinator,Directie")]
        public async Task<IActionResult> Archief()
        {
           
            var studiebezoeken = await _context.Studiebezoeken
                .Include(s => s.Aanvrager)
                .Include(s => s.Vak)
                .Include(s => s.Bijlages)
                .Include(s => s.Begeleiders)
                .Include(s => s.FotoAlbums)
                .Include(s => s.Klassen)
                .Where(s => s.IsAfgewerkt == true)                
                .OrderByDescending(sb => sb.Datum)
                .Select(sb => new StudiebezoekViewModel(sb))
                .ToListAsync();

            return View(studiebezoeken);
        }


        // GET: Studiebezoeken/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Referer"] = Request.Headers["Referer"].ToString();

            if (id == null || _context.Studiebezoeken == null)
            {
                return NotFound();
            }

            var viewModel = await _context.Studiebezoeken
                .Include(s => s.Aanvrager)
                .Include(s => s.Vak)
                .Include(s => s.Bijlages)
                .Include(s => s.Begeleiders)
                .Include(s => s.FotoAlbums)
                .Include(s => s.Klassen)
                .Where(m => m.Id == id)
                .Select(sb => new StudiebezoekDetailsViewModel(sb))
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Studiebezoeken/Create

        public async Task<IActionResult> Create()
        {

            var leerkrachten = await _userManager.GetUsersInRoleAsync("Leerkracht");
            var actieveLeerkrachten = leerkrachten.Where(l => !l.Verwijderd).ToList();
            var actieveKlassen = await _context.Klassen
                                   .Where(k => k.Verwijderd == false)
                                   .ToListAsync();
            var actieveVakken = await _context.Vakken
                                   .Where(v => v.Verwijderd == false)
                                   .ToListAsync();

            var viewModel = new StudiebezoekCreateViewModel
            {
                MogelijkeBegeleiders = actieveLeerkrachten,
                MogelijkOrganiserendeVakken = new SelectList(actieveVakken, "Id", "Naam"),
                MogelijkeDeelnemendeKlassen = new SelectList(actieveKlassen, "Id", "Naam")
            };

            return View(viewModel);
        }

        // POST: Studiebezoeken/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudiebezoekCreateViewModel viewModel)
        {
            var gebruiker = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Leerkrachten = await _userManager.GetUsersInRoleAsync("Leerkracht");
            var actieveLeerkrachten = Leerkrachten.Where(l => !l.Verwijderd).ToList();

            var actieveKlassen = await _context.Klassen
                                   .Where(k => k.Verwijderd == false)
                                   .ToListAsync();
            var actieveVakken = await _context.Vakken
                                   .Where(v => v.Verwijderd == false)
                                   .ToListAsync();
            //aanvrager koppelen aan ingelogde gebruiker.
            viewModel.Aanvrager = gebruiker;
            viewModel.AanvragerId = gebruiker.Id;
            //datum van de uitstappen koppelen aan de start en einduur
            viewModel.StartUur = new DateTime(viewModel.Datum.Year, viewModel.Datum.Month, viewModel.Datum.Day,
                               viewModel.StartUur.Hour, viewModel.StartUur.Minute, 0);
            viewModel.EindUur = new DateTime(viewModel.Datum.Year, viewModel.Datum.Month, viewModel.Datum.Day,
                                           viewModel.EindUur.Hour, viewModel.EindUur.Minute, 0);

            await CheckForDoubles(viewModel);
            ValidateStudiebezoek(viewModel);

            //niet belangrijk voor de gebruiker enkel voor debuggen.
            DebugModelState(viewModel);

            if (ModelState.IsValid)
            {
                var studiebezoek = new Studiebezoek
                {
                    Id = viewModel.Id,
                    AanvragerId = viewModel.AanvragerId,
                    VakId = viewModel.VakId,
                    Datum = viewModel.Datum,
                    StartUur = viewModel.StartUur,
                    EindUur = viewModel.EindUur,
                    Reden = viewModel.Reden ?? "nutteloze null coalesing om warning weg te krijgen",
                    AantalStudenten = viewModel.AantalStudenten,
                    KostprijsStudiebezoek = viewModel.KostprijsStudiebezoek,
                    VervoerBus = viewModel.VervoerBus,
                    VervoerTram = viewModel.VervoerTram,
                    VervoerTrein = viewModel.VervoerTrein,
                    VervoerTeVoet = viewModel.VervoerTeVoet,
                    VervoerAuto = viewModel.VervoerAuto,
                    VervoerFiets = viewModel.VervoerFiets,
                    KostprijsVervoer = viewModel.KostprijsVervoer,
                    Opmerkingen = viewModel.Opmerkingen,
                    Begeleiders = new List<Gebruiker>(),
                    Klassen = new List<Klas>(),
                    Bijlages = new List<Bijlage>()
                };

                if (viewModel.Bijlages != null && viewModel.Bijlages.Count > 0)
                {
                    // Map voor bijlagen controleren en aanmaken
                    var bijlagenMap = Path.Combine(_hostingEnvironment.WebRootPath, "bijlagen");
                    if (!Directory.Exists(bijlagenMap))
                    {
                        Directory.CreateDirectory(bijlagenMap);
                    }

                    // Map voor specifiek studiebezoek aanmaken
                    var vakNaam = _context.Vakken.Find(viewModel.VakId)?.Naam ?? "Onbekend";
                    var aanvragerInitialen = _context.Gebruikers.Find(viewModel.AanvragerId)?.Initialen ?? "Onbekend";
                    var studiebezoekMapNaam =
                        $"stube{viewModel.Datum:yyyyMMdd}{vakNaam.Replace(" ","")}{aanvragerInitialen}{viewModel.StartUur:HHmm}{viewModel.EindUur:HHmm}";

                    var studiebezoekMap = Path.Combine(bijlagenMap, studiebezoekMapNaam);
                    if (!Directory.Exists(studiebezoekMap))
                    {
                        Directory.CreateDirectory(studiebezoekMap);
                    }



                    foreach (var file in viewModel.Bijlages)
                    {
                        if (file.Length > 0)
                        {
                            var filePath = Path.Combine(studiebezoekMap, file.FileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            // Maak een nieuw Bijlage object
                            var bijlage = new Bijlage
                            {
                                StudiebezoekId = studiebezoek.Id,
                                BestandsNaam = studiebezoekMapNaam + "_split_" + file.FileName
                            };

                            // Voeg bijlage toe aan studiebezoek
                            studiebezoek.Bijlages.Add(bijlage);
                        }
                    }
                }



                // Handle Begeleiders
                //if functie is enkel voor warning die eigelijk al afgehandeld is in mijn valideer weg te krijgen dus redelijk nutteloos buiten warning wegkrijgen.
                if (viewModel.GeselecteerdeBegeleiderIds != null)
                {
                    foreach (var begeleiderId in viewModel.GeselecteerdeBegeleiderIds)
                    {
                        var begeleider = actieveLeerkrachten.FirstOrDefault(g => g.Id == begeleiderId);
                        if (begeleider != null)
                        {
                            studiebezoek.Begeleiders.Add(begeleider);
                        }
                    }
                }


                // Handle Klassen
                if (viewModel.GeselecteerdeKlasIds != null)
                {
                    foreach (var klasId in viewModel.GeselecteerdeKlasIds)
                    {
                        var klas = actieveKlassen.FirstOrDefault(k => k.Id == int.Parse(klasId));
                        if (klas != null)
                        {
                            studiebezoek.Klassen.Add(klas);
                        }
                    }
                }

                _context.Add(studiebezoek);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            viewModel.Aanvrager = gebruiker;
            viewModel.MogelijkeBegeleiders = actieveLeerkrachten;
            viewModel.MogelijkOrganiserendeVakken = new SelectList(actieveVakken, "Id", "Naam", viewModel.VakId);
            viewModel.MogelijkeDeelnemendeKlassen = new SelectList(actieveKlassen, "Id", "Naam");

            return View(viewModel);
        }

        [Authorize(Roles = "Beheerder,Coördinator,Directie")]
        // GET: Studiebezoeken/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Studiebezoeken == null)
            {
                return NotFound();
            }
            var Leerkrachten = await _userManager.GetUsersInRoleAsync("Leerkracht");
            var actieveLeerkrachten = Leerkrachten.Where(l => !l.Verwijderd).ToList();

            var actieveKlassen = await _context.Klassen
                                   .Where(k => k.Verwijderd == false)
                                   .ToListAsync();
            var actieveVakken = await _context.Vakken
                                   .Where(v => v.Verwijderd == false)
                                   .ToListAsync();
            var viewModel = new StudiebezoekEditViewModel();
            var studiebezoek = await _context.Studiebezoeken
                .Include(s => s.Aanvrager)
                .Include(s => s.Vak)
                .Include(s => s.Bijlages)
                .Include(s => s.Begeleiders)
                .Include(s => s.FotoAlbums)
                .Include(s => s.Klassen)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (studiebezoek == null)
            {
                return NotFound();
            }
            viewModel.InitializeFromStudiebezoek(studiebezoek);

            viewModel.MogelijkeBegeleiders = new SelectList(actieveLeerkrachten, "Id", "VolledigeNaam");
            viewModel.MogelijkOrganiserendeVakken = new SelectList(actieveVakken, "Id", "Naam");
            viewModel.MogelijkeDeelnemendeKlassen = new SelectList(actieveKlassen, "Id", "Naam");
            viewModel.IsGoedgekeurdCoSelectList = GetBooleanSelectList(viewModel.IsGoedgekeurdCo);
            viewModel.IsGoedgekeurdDirSelectList = GetBooleanSelectList(viewModel.IsGoedgekeurdDir);
            viewModel.IsAfgewezenSelectList = GetBooleanSelectList(viewModel.IsAfgewezen);
            viewModel.IsAfgewerktSelectList = GetBooleanSelectListJaNee(viewModel.IsAfgewerkt);
            if(viewModel.Begeleiders != null)
            {
                viewModel.GeselecteerdeBegeleiderIds = viewModel.Begeleiders.Select(b => b.Id).ToList();
            }
            else
            {
                viewModel.GeselecteerdeBegeleiderIds = new List<String>();
            }
            
            if(viewModel.Klassen != null)
            {
                viewModel.GeselecteerdeKlasIds = viewModel.Klassen.Select(k => k.Id.ToString()).ToList();
            }
            else
            {
                viewModel.GeselecteerdeKlasIds = new List<String>();
            }
            


            return View(viewModel);
        }

        // POST: Studiebezoeken/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudiebezoekEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }


            var studiebezoek = await _context.Studiebezoeken
                .Include(s => s.Aanvrager)
                .Include(s => s.Vak)
                .Include(s => s.Bijlages)
                .Include(s => s.Begeleiders)
                .Include(s => s.FotoAlbums)
                .Include(s => s.Klassen)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (studiebezoek == null)
            {
                return NotFound();
            }

            var Leerkrachten = await _userManager.GetUsersInRoleAsync("Leerkracht");
            var actieveLeerkrachten = Leerkrachten.Where(l => !l.Verwijderd).ToList();

            var actieveKlassen = await _context.Klassen
                                   .Where(k => k.Verwijderd == false)
                                   .ToListAsync();
            var actieveVakken = await _context.Vakken
                                   .Where(v => v.Verwijderd == false)
                                   .ToListAsync();

            //verzekeren dat startuur en einduur aan datum is gekoppeled
            viewModel.StartUur = new DateTime(viewModel.Datum.Year, viewModel.Datum.Month, viewModel.Datum.Day,
                               viewModel.StartUur.Hour, viewModel.StartUur.Minute, 0);
            viewModel.EindUur = new DateTime(viewModel.Datum.Year, viewModel.Datum.Month, viewModel.Datum.Day,
                                           viewModel.EindUur.Hour, viewModel.EindUur.Minute, 0);

            ValidateStudiebezoek(viewModel);
            DebugModelState(viewModel);


            if (ModelState.IsValid)
            {
                //update eigenaschappen van studiebezoek
                studiebezoek.VakId = viewModel.VakId;
                studiebezoek.Datum = viewModel.Datum;
                studiebezoek.StartUur = viewModel.StartUur;
                studiebezoek.EindUur = viewModel.EindUur;
                studiebezoek.Reden = viewModel.Reden;
                studiebezoek.AantalStudenten = viewModel.AantalStudenten;
                studiebezoek.KostprijsStudiebezoek = viewModel.KostprijsStudiebezoek;
                studiebezoek.VervoerBus = viewModel.VervoerBus;
                studiebezoek.VervoerTram = viewModel.VervoerTram;
                studiebezoek.VervoerTrein = viewModel.VervoerTrein;
                studiebezoek.VervoerTeVoet = viewModel.VervoerTeVoet;
                studiebezoek.VervoerAuto = viewModel.VervoerAuto;
                studiebezoek.VervoerFiets = viewModel.VervoerFiets;
                studiebezoek.KostprijsVervoer = viewModel.KostprijsVervoer;
                studiebezoek.AfwezigeStudenten = viewModel.AfwezigeStudenten;
                studiebezoek.Opmerkingen = viewModel.Opmerkingen;
                studiebezoek.IsGoedgekeurdCo = viewModel.IsGoedgekeurdCo;
                studiebezoek.IsGoedgekeurdDir = viewModel.IsGoedgekeurdDir;
                studiebezoek.IsAfgewezen = viewModel.IsAfgewezen;
                studiebezoek.OpmerkingAfgewezen = viewModel.OpmerkingAfgewezen;
                studiebezoek.IsAfgewerkt = viewModel.IsAfgewerkt;
                

                //update associaties m2m
                // Bijwerken van begeleiders if functie dient enkel voor warning weg te krijgen
                if (viewModel.GeselecteerdeBegeleiderIds != null)
                {
                    studiebezoek.Begeleiders.Clear();
                    foreach (var begeleiderId in viewModel.GeselecteerdeBegeleiderIds)
                    {
                        var begeleider = await _context.Gebruikers.FindAsync(begeleiderId);
                        if (begeleider != null)
                        {
                            studiebezoek.Begeleiders.Add(begeleider);
                        }
                    }
                }

                // Bijwerken van klassen if functie is enkel voor waarning weg te krijgen
                if (viewModel.GeselecteerdeKlasIds != null)
                {
                    studiebezoek.Klassen.Clear();
                    foreach (var klasId in viewModel.GeselecteerdeKlasIds)
                    {
                        var klas = await _context.Klassen.FindAsync(int.Parse(klasId));
                        if (klas != null)
                        {
                            studiebezoek.Klassen.Add(klas);
                        }
                    }
                }

                //bijlages toevoegen
                if (viewModel.ExtraBijlages != null && viewModel.ExtraBijlages.Count() > 0)
                {
                    // Map voor bijlagen controleren en aanmaken (main folder)
                    var vakNaam = "";
                    var aanvragerInitialen ="";
                    var studiebezoekMapNaam = "";
                    var bijlagenMap = Path.Combine(_hostingEnvironment.WebRootPath, "bijlagen");
                    if (!Directory.Exists(bijlagenMap))
                    {
                        Directory.CreateDirectory(bijlagenMap);
                    }
                    
                    if (studiebezoek.Bijlages != null && studiebezoek.Bijlages.Count() > 0 && (!string.IsNullOrWhiteSpace(studiebezoek.Bijlages[0].Foldernaam)))
                    {
                          studiebezoekMapNaam = studiebezoek.Bijlages[0].Foldernaam;
                    }
                    else
                    {

                         vakNaam = _context.Vakken.Find(viewModel.VakId)?.Naam ?? "Onbekend";                        
                         aanvragerInitialen = _context.Gebruikers.Find(studiebezoek.AanvragerId)?.Initialen ?? "Onbekend";
                         studiebezoekMapNaam =
                            $"stube{viewModel.Datum:yyyyMMdd}{vakNaam}{aanvragerInitialen}{viewModel.StartUur:HHmm}{viewModel.EindUur:HHmm}";
                    }                   
                                        
                    //als map niet bestaat aanmaken
                    var studiebezoekMap = Path.Combine(bijlagenMap, studiebezoekMapNaam);
                    if (!Directory.Exists(studiebezoekMap))
                    {
                        Directory.CreateDirectory(studiebezoekMap);
                    }



                    foreach (var file in viewModel.ExtraBijlages)
                    {
                        if (file.Length > 0)
                        {
                            var filePath = Path.Combine(studiebezoekMap, file.FileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            // Maak een nieuw Bijlage object
                            var bijlage = new Bijlage
                            {
                                StudiebezoekId = studiebezoek.Id,
                                BestandsNaam = studiebezoekMapNaam + "_split_" + file.FileName
                            };

                            // Voeg bijlage toe aan studiebezoek
                            if(studiebezoek.Bijlages == null)
                            {
                                studiebezoek.Bijlages = new List<Bijlage>();
                            }                               
                            
                            studiebezoek.Bijlages.Add(bijlage);
                        }
                    }
                }

                if(viewModel.FilesToDelete != null)
                {
                    foreach (var bijlageId in viewModel.FilesToDelete)
                    {
                        var bijlage = await _context.Bijlages.FindAsync(bijlageId);
                        if (bijlage != null)
                        {

                            if (!string.IsNullOrEmpty(bijlage.Foldernaam))
                            {



                                // Verwijder het bestand uit de server
                                var bestandsPath = Path.Combine(_hostingEnvironment.WebRootPath, "bijlagen", bijlage.Foldernaam, bijlage.EchteBestandsNaam);
                                if (System.IO.File.Exists(bestandsPath))
                                {
                                    System.IO.File.Delete(bestandsPath);
                                }

                                // Als het de laatste bijlage in de map is, verwijder dan de map
                                var folderPath = Path.GetDirectoryName(bestandsPath);
                                if (Directory.Exists(folderPath) && !Directory.EnumerateFiles(folderPath).Any())
                                {
                                    Directory.Delete(folderPath);
                                }
                            }
                            // Verwijder uit de database
                            _context.Bijlages.Remove(bijlage);
                        }
                    }
                }


                try
                {
                    _context.Update(studiebezoek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudiebezoekExists(studiebezoek.Id))
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
            viewModel.InitializeFromStudiebezoek(studiebezoek);
            viewModel.MogelijkeBegeleiders = new SelectList(actieveLeerkrachten, "Id", "VolledigeNaam");
            viewModel.MogelijkOrganiserendeVakken = new SelectList(actieveVakken, "Id", "Naam");
            viewModel.MogelijkeDeelnemendeKlassen = new SelectList(actieveKlassen, "Id", "Naam");
            viewModel.IsGoedgekeurdCoSelectList = GetBooleanSelectList(viewModel.IsGoedgekeurdCo);
            viewModel.IsGoedgekeurdDirSelectList = GetBooleanSelectList(viewModel.IsGoedgekeurdDir);
            viewModel.IsAfgewezenSelectList = GetBooleanSelectList(viewModel.IsAfgewezen);
            viewModel.IsAfgewerktSelectList = GetBooleanSelectListJaNee(viewModel.IsAfgewerkt);

            if(viewModel.Begeleiders != null)
            {
                viewModel.GeselecteerdeBegeleiderIds = viewModel.Begeleiders.Select(b => b.Id).ToList();
            }
            else
            {
                viewModel.GeselecteerdeBegeleiderIds = new List<String>();
            }
            
            viewModel.GeselecteerdeKlasIds = viewModel?.Klassen?.Select(k => k.Id.ToString()).ToList();
                                                                                                      


            return View(viewModel);
        }

        [Authorize(Roles = "Beheerder")]
        // GET: Studiebezoeken/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Studiebezoeken == null)
            {
                return NotFound();
            }

            var viewModel = await _context.Studiebezoeken
                .Include(s => s.Aanvrager)
                .Include(s => s.Vak)
                .Include(s => s.Bijlages)
                .Include(s => s.Begeleiders)
                .Include(s => s.FotoAlbums)
                .Include(s => s.Klassen)
                .Where(m => m.Id == id)
                .Select(sb => new StudiebezoekDetailsViewModel(sb))
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);

        }

        // POST: Studiebezoeken/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Studiebezoeken == null)
            {
                return Problem("Entity set 'AzureDbContext.Studiebezoeken'  is null.");
            }
            //var studiebezoek = await _context.Studiebezoeken.FindAsync(id);
            var studiebezoek = await _context.Studiebezoeken
                    .Include(s => s.Vak)
                    .Include(s => s.Aanvrager)
                    .FirstOrDefaultAsync(m => m.Id == id);

            if (studiebezoek != null)
            {

                //var vak = _context.Vakken.Where(v => v.Id == studiebezoek.VakId);
                //var aanvrager = _context.Gebruikers.Where(g => g.Id == studiebezoek.AanvragerId);
                var klasStudiebezoeken =  _context.KlasStudiebezoeken.Where(k => k.StudiebezoekId == id);
                var begeleidingen = _context.Begeleidingen.Where(b => b.StudiebezoekId == id);
                var bijlagen = _context.Bijlages.Where(bl => bl.StudiebezoekId == id);

                if(bijlagen != null && bijlagen.Count() > 0)
                {
                    var vakNaam = studiebezoek.Vak.Naam ?? "Onbekend";

                    var aanvragerInitialen = studiebezoek.Aanvrager?.Initialen ?? "Onbekend";
                    var studiebezoekMapNaam = bijlagen.FirstOrDefault()?.Foldernaam ?? "";
                    
                    if (string.IsNullOrWhiteSpace(studiebezoekMapNaam))
                    {
                        studiebezoekMapNaam = $"stube{studiebezoek.Datum:yyyyMMdd}{vakNaam.Replace(" ", "")}{aanvragerInitialen}{studiebezoek.StartUur:HHmm}{studiebezoek.EindUur:HHmm}";
                    }

                    var studiebezoekMap = Path.Combine(_hostingEnvironment.WebRootPath, "bijlagen", studiebezoekMapNaam);
                    if (Directory.Exists(studiebezoekMap) && !string.IsNullOrWhiteSpace(studiebezoekMapNaam))
                    {
                        Directory.Delete(studiebezoekMap, true); // true om alle submappen en bestanden te verwijderen
                    }
                }              
                             



                //assosiatie tabellen met restricted verwijderen
                _context.KlasStudiebezoeken.RemoveRange(klasStudiebezoeken);
                _context.Begeleidingen.RemoveRange(begeleidingen);

                if(bijlagen != null)
                    _context.Bijlages.RemoveRange(bijlagen);
                
                

                _context.Studiebezoeken.Remove(studiebezoek);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Coördinator")]
        [HttpPost]
        public IActionResult ApproveCo(int id)
        {
            var studiebezoek = _context.Studiebezoeken.Find(id);

            if (studiebezoek == null)
            {
                return NotFound();
            }

            // Set IsGoedgekeurdCo to true
            studiebezoek.IsGoedgekeurdCo = true;

            // Update the entity in the database
            _context.Studiebezoeken.Update(studiebezoek);
            _context.SaveChanges();

            // Redirect to an appropriate action after approval
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Directie")]
        [HttpPost]
        public async Task<IActionResult> ApproveDir(int id)
        {
            var studiebezoek = _context.Studiebezoeken.Find(id);

            if (studiebezoek == null)
            {
                return NotFound();
            }


            // Set IsGoedgekeurdDir to true
            studiebezoek.IsGoedgekeurdDir = true;
            studiebezoek.IsAfgewezen = false;

            // Update the entity in the database
            _context.Studiebezoeken.Update(studiebezoek);
            _context.SaveChanges();

            //mail verzenden (zet recieverEmail op je eigen mail voor te testen)
            studiebezoek.Aanvrager = await _userManager.FindByIdAsync(studiebezoek.AanvragerId);

            string senderEmail = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result.Email;
            string senderName = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result.VolledigeNaam;
            string recieverEmail = studiebezoek.Aanvrager.Email;
            //string recieverEmail = "test@example.com";
            string recieverName = studiebezoek.Aanvrager.VolledigeNaam;
            string subject = $"Goedkeuring studiebezoek '{studiebezoek.Reden}'";
            string message = $"Beste {recieverName},\n\nje aanvraag voor een studiebezoek met reden '{studiebezoek.Reden}' op datum van {studiebezoek.Datum} is goedgekeurd.";
            message += "\n\nMet vriendelijke groentjes\n";
            message += senderName;

            EmailSender.SendEmail(senderEmail, senderName, recieverEmail, recieverName, subject, message);


            //afwezigheden aanmaken

            var begeleidingen = _context.Begeleidingen.Where(b => b.StudiebezoekId == id).ToList();
            if (begeleidingen != null)
            {
                foreach (var beg in begeleidingen)
                {
                    await SetBegeleiderAfwezig(beg.GebruikerId, studiebezoek.StartUur, studiebezoek.EindUur);
                }
            }
            

            // Redirect to an appropriate action after approval
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Directie,Coördinator")]
        [HttpPost]
        public async Task<IActionResult> Refuse(int id, string afkeurReden)
        {
            var studiebezoek = _context.Studiebezoeken.Find(id);

            if (studiebezoek == null)
            {
                return NotFound();
            }

            // Update OpmerkingAfgewezen with the entered reason
            studiebezoek.OpmerkingAfgewezen = afkeurReden;

            // Set IsGoedGekeurd to false
            if(studiebezoek.IsGoedgekeurdCo == null)
                studiebezoek.IsGoedgekeurdCo = false;
            else
                studiebezoek.IsGoedgekeurdDir = false;

            studiebezoek.IsAfgewezen = true;

            // Update the entity in the database
            _context.Studiebezoeken.Update(studiebezoek);
            _context.SaveChanges();

            //mail verzenden (zet recieverEmail op je eigen mail voor te testen)
            studiebezoek.Aanvrager = await _userManager.FindByIdAsync(studiebezoek.AanvragerId);

            string senderEmail = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result.Email;
            string senderName = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result.VolledigeNaam;
            string recieverEmail = studiebezoek.Aanvrager.Email;
            //string recieverEmail = "test@example.com";
            string recieverName = studiebezoek.Aanvrager.VolledigeNaam;
            string subject = $"Afkeuring navorming '{studiebezoek.Reden}'";
            string message = $"Beste {recieverName},\n\ntot mijn grote spijt heb ik het studiebezoek met reden '{studiebezoek.Reden}' op datum van {studiebezoek.Datum} moeten afkeuren omwille van volgende reden:\n\n";
            message += afkeurReden;
            message += "\n\nMet vriendelijke groeten\n";
            message += senderName;

            EmailSender.SendEmail(senderEmail, senderName, recieverEmail, recieverName, subject, message);

            // Redirect to an appropriate action after disapproval
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> IsCompleted(int id, string absentStudents)
        {
            var studiebezoek = await _context.Studiebezoeken.FindAsync(id);

            if (studiebezoek == null)
            {
                return NotFound();
            }

            studiebezoek.IsAfgewerkt = true;
            studiebezoek.IsAfgewerkt = true;
            if (!string.IsNullOrWhiteSpace(absentStudents))
            {
                studiebezoek.AfwezigeStudenten = absentStudents;
            }

            // Update the entity in the database
            _context.Studiebezoeken.Update(studiebezoek);
            _context.SaveChanges();

            // Redirect to an appropriate action after approval

            var viewModel = await _context.Studiebezoeken
                .Include(s => s.Aanvrager)
                .Include(s => s.Vak)
                .Include(s => s.Bijlages)
                .Include(s => s.Begeleiders)
                .Include(s => s.FotoAlbums)
                .Include(s => s.Klassen)
                .Where(m => m.Id == id)
                .Select(sb => new StudiebezoekDetailsViewModel(sb))
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound();
            }

            return View("Details",viewModel);
        }

        private bool StudiebezoekExists(int id)
        {
            return (_context.Studiebezoeken?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private async Task CheckForDoubles(StudiebezoekCreateViewModel viewModel)
        {

            //validatie die dient om te kijken of er al een studiebezoek is aangemaakt door de aanvrager op dezelfde datum en tijd voor hetzelde vak, zodat er zeker geen dubbele foldernamen kunnen bestaand voor bijlages.
            var bestaandStudiebezoek = await _context.Studiebezoeken
                        .AnyAsync(sb => sb.VakId == viewModel.VakId &&
                        sb.AanvragerId == viewModel.AanvragerId &&
                        sb.Datum == viewModel.Datum &&
                        sb.StartUur == viewModel.StartUur &&
                        sb.EindUur == viewModel.EindUur);

            if (bestaandStudiebezoek)
            {
                ModelState.AddModelError("", "U heeft reeds een studiebezoek voor dit vak aangevraagd op deze datum en tijd.");
            }
        }
        private void ValidateStudiebezoek(IStudiebezoekViewModel viewModel)
        {

            if (string.IsNullOrWhiteSpace(viewModel.Reden))
            {
                ModelState.AddModelError("Reden", "Reden is verplicht in te vullen.");
            }

            if (viewModel.StartUur >= viewModel.EindUur)
            {
                ModelState.AddModelError("EindUur", "Eindtijd moet later zijn dan de starttijd.");
            }


            if (viewModel.AantalStudenten == null || viewModel.AantalStudenten < 1)
            {
                ModelState.AddModelError("AantalStudenten", "Gelieve een correct aantal student in te geven.");
            }

            if (viewModel.VakId == 0)
            {
                ModelState.AddModelError("VakId", "Vak is verplicht.");
            }

            if (viewModel.GeselecteerdeBegeleiderIds == null)
            {
                ModelState.AddModelError("GeselecteerdeBegeleiderIds", "Gelieve minstens 1 begeleider te selecteren. Gebruik Ctrl-klik om meerdere begeleiders te selecteren");
            }

            if (viewModel.GeselecteerdeKlasIds == null)
            {
                ModelState.AddModelError("GeselecteerdeKlasIds", "Gelieve minstens 1 klas te selecteren. Gebruik Ctrl-klik om meerdere klassen te selecteren");
            }

            if (!viewModel.VervoerAuto &&
                !viewModel.VervoerBus &&
                !viewModel.VervoerTram &&
                !viewModel.VervoerTrein &&
                !viewModel.VervoerTeVoet &&
                !viewModel.VervoerFiets)
            {
                ModelState.AddModelError("VervoerFiets", "Gelieve minstens 1 vervoersmiddel te selecteren.");
            }

            if ((viewModel.VervoerTram || viewModel.VervoerTrein) && viewModel.KostprijsVervoer == null)
            {
                ModelState.AddModelError("KostprijsVervoer", "Gelieve een vervoersprijs in te voeren wanneer Tram of Trein geselecteerd zijn.");
            }



        }
        private async Task SetBegeleiderAfwezig(string begeleiderId, DateTime startDatum, DateTime eindDatum)
        {
            Afwezigheid afwezigheid = new Afwezigheid
            {
                GebruikerId = begeleiderId,
                StartDatum = startDatum,
                EindDatum = eindDatum
            };

            _context.Add(afwezigheid);
            await _context.SaveChangesAsync();
        }
        private SelectList GetBooleanSelectList(bool? currentValue)
        {
            var selectList = new List<SelectListItem>
                {
                      new SelectListItem { Text = "In behandeling", Value = "", Selected = currentValue == null },
                      new SelectListItem { Text = "Ja", Value = "true", Selected = currentValue == true },
                      new SelectListItem { Text = "Nee", Value = "false", Selected = currentValue == false }
                };

            return new SelectList(selectList, "Value", "Text");
        }
        private SelectList GetBooleanSelectListJaNee(bool? currentValue)
        {
            var selectList = new List<SelectListItem>
                {
                     new SelectListItem { Text = "Ja", Value = "true", Selected = currentValue == true },
                     new SelectListItem { Text = "Nee", Value = "false", Selected = currentValue == false || currentValue == null }
                };

            return new SelectList(selectList, "Value", "Text");
        }
        private void DebugModelState(IStudiebezoekViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                System.Diagnostics.Debug.WriteLine($"INVALID INVALID INVALID INVALID INVALID");
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        // 'entry.Key' geeft veldnaam
                        System.Diagnostics.Debug.WriteLine($"Field: {entry.Key}");

                        foreach (var error in entry.Value.Errors)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error: {error.ErrorMessage}");
                        }
                    }
                }
            }
        }            

    }
}