#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using McCarthy_Week10.Data;
using McCarthy_Week10.Models;

namespace McCarthy_Week10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CulturesController : ControllerBase
    {
        private readonly Adventureworks2019Context _context;

        public CulturesController(Adventureworks2019Context context)
        {
            _context = context;
        }

        // GET: api/Cultures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Culture>>> GetCultures()
        {
            return await _context.Cultures.ToListAsync();
        }

        // GET: api/Cultures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Culture>> GetCulture(string id)
        {
            var culture = await _context.Cultures.FindAsync(id);

            if (culture == null)
            {
                return NotFound();
            }

            return culture;
        }

        // PUT: api/Cultures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCulture(string id, Culture culture)
        {
            if (id != culture.CultureId)
            {
                return BadRequest();
            }

            _context.Entry(culture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CultureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cultures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Culture>> PostCulture(Culture culture)
        {
            _context.Cultures.Add(culture);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CultureExists(culture.CultureId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCulture", new { id = culture.CultureId }, culture);
        }

        // DELETE: api/Cultures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCulture(string id)
        {
            var culture = await _context.Cultures.FindAsync(id);
            if (culture == null)
            {
                return NotFound();
            }

            _context.Cultures.Remove(culture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CultureExists(string id)
        {
            return _context.Cultures.Any(e => e.CultureId == id);
        }
    }
}
