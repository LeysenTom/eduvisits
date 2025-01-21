using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Controllers.API.APIBase;
using MVCProject.Data;
using MVCProject.Models;
using NuGet.Common;

namespace MVCProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [APIkey]
    [AllowAnonymous]
    public class AfwezigheidController : ControllerBase
    {
        private readonly AzureDbContext _context;
        private readonly UserManager<Gebruiker> _userManager;

        public AfwezigheidController(AzureDbContext context, UserManager<Gebruiker> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Afwezigheid
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIAfwezigheidModel>>> GetAfwezighedenDefault()
        {
            List<APIAfwezigheidModel> afwezigheden = await GetAfwezigheden(0);

            if (afwezigheden == null)
            {
                return NotFound();
            }

            return afwezigheden;
        }

        //GET: api/Afwezigheid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<APIAfwezigheidModel>>> GetAfwezighedenByWeekDifference(int id)
        {
            List<APIAfwezigheidModel> afwezigheden = await GetAfwezigheden(id);

            if (afwezigheden == null)
            {
                return NotFound();
            }

            return afwezigheden;
        }

        public async Task<List<APIAfwezigheidModel>> GetAfwezigheden(int week)
        {
            if (_context.Afwezigheden == null)
            {
                return null;
            }

            //data verkleinen (geen gevoelige info meegeven)
            List<APIAfwezigheidModel> afwezigheden = new List<APIAfwezigheidModel>();

            // get deze week + verschil opvraging + 100 toe voegen of aftrekken voor jaar verschil
            int weeknumber = System.Globalization.ISOWeek.GetWeekOfYear(DateTime.Now.AddDays(7 * week)) + 100 * (DateTime.Now.AddDays(7 * week).Year - DateTime.Now.Year);

            //alle afwezigheden ; system.Globalization werkt niet in .where()
            List<Afwezigheid> AlleAfwezigheden = await _context.Afwezigheden.Include(a => a.Gebruiker).ToListAsync();

            //filteren op week
            foreach (Afwezigheid item in AlleAfwezigheden)
            {
                //controleer of startdatum < eindatum, anders flip
                if (!(item.StartDatum < item.EindDatum))
                {
                    DateTime start = item.EindDatum;
                    item.EindDatum = item.StartDatum;
                    item.StartDatum = start;
                }

                // week nummer + 100 * jaar
                if (System.Globalization.ISOWeek.GetWeekOfYear(item.EindDatum) + 100 * (item.EindDatum.Year - DateTime.Now.Year) >= weeknumber
                    && System.Globalization.ISOWeek.GetWeekOfYear(item.StartDatum) + 100 * (item.StartDatum.Year - DateTime.Now.Year) <= weeknumber)
                {
                    //ophalen rol //overgenomen van accountcontroller
                    var rollen = await _userManager.GetRolesAsync(item.Gebruiker);
                    string rollenString = "";
                    for (int i = 0; i < rollen.Count(); i++)
                    {
                        rollenString += rollen[i];
                        if (i < rollen.Count() - 1)
                            rollenString += ", ";
                    }
                    //aanpassen aan API model
                    APIAfwezigheidModel model = new APIAfwezigheidModel()
                    {
                        GebruikerNaam = item.Gebruiker.VolledigeNaam,
                        GebruikerRol = rollenString,
                        StartDatum = item.StartDatum,
                        EindDatum = item.EindDatum
                    };

                    afwezigheden.Add(model);
                }
            }

            return afwezigheden;
        }

        // PUT: api/Afwezigheid/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAfwezigheid(int id, Afwezigheid afwezigheid)
        //{
        //    if (id != afwezigheid.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(afwezigheid).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AfwezigheidExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Afwezigheid
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Afwezigheid>> PostAfwezigheid(Afwezigheid afwezigheid)
        //{
        //    if (_context.Afwezigheden == null)
        //    {
        //        return Problem("Entity set 'AzureDbContext.Afwezigheden'  is null.");
        //    }
        //    _context.Afwezigheden.Add(afwezigheid);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAfwezigheid", new { id = afwezigheid.Id }, afwezigheid);
        //}

        // DELETE: api/Afwezigheid/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAfwezigheid(int id)
        //{
        //    if (_context.Afwezigheden == null)
        //    {
        //        return NotFound();
        //    }
        //    var afwezigheid = await _context.Afwezigheden.FindAsync(id);
        //    if (afwezigheid == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Afwezigheden.Remove(afwezigheid);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool AfwezigheidExists(int id)
        {
            return (_context.Afwezigheden?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}