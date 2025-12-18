using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkordideApi.Data;
using AkordideApi.Models;

namespace AkordideApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoodController : ControllerBase
    {
        private readonly Data.AkordContext _context;

        public LoodController(Data.AkordContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lugu>>> GetAll()
        {
            return await _context.Lood.Include(l => l.Taktid).ThenInclude(t => t.Kolmkola).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lugu>> Get(int id)
        {
            var l = await _context.Lood.Include(x => x.Taktid).ThenInclude(t => t.Kolmkola).FirstOrDefaultAsync(x => x.Id == id);
            if (l == null) return NotFound(new { message = "Lugu ei leitud." });
            return l;
        }

        [HttpPost]
        public async Task<ActionResult<Lugu>> Create([FromBody] Lugu lugu)
        {
            _context.Lood.Add(lugu);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = lugu.Id }, lugu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Lugu lugu)
        {
            var ex = await _context.Lood.Include(x => x.Taktid).FirstOrDefaultAsync(x => x.Id == id);
            if (ex == null) return NotFound(new { message = "Lugu ei leitud." });

            ex.Nimetus = lugu.Nimetus;

            // 1. Eemalda need taktid, mida uues listis pole
            var toRemove = ex.Taktid.Where(t => !lugu.Taktid.Any(l => l.Id == t.Id)).ToList();
            foreach (var r in toRemove)
            {
                ex.Taktid.Remove(r);
            }

            // 2. Uuenda olemasolevaid või lisa uued
            foreach (var incomingTakt in lugu.Taktid)
            {
                var existingTakt = ex.Taktid.FirstOrDefault(t => t.Id == incomingTakt.Id && t.Id != 0);
                if (existingTakt != null)
                {
                    existingTakt.KolmkolaId = incomingTakt.KolmkolaId;
                }
                else
                {
                    ex.Taktid.Add(incomingTakt);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ex = await _context.Lood.FindAsync(id);
            if (ex == null) return NotFound(new { message = "Lugu ei leitud." });

            _context.Lood.Remove(ex);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/noodid")]
        public async Task<ActionResult> GetNoodid(int id, [FromQuery] string formaad = "arv")
        {
            var l = await _context.Lood.Include(x => x.Taktid).ThenInclude(t => t.Kolmkola).FirstOrDefaultAsync(x => x.Id == id);
            if (l == null) return NotFound(new { message = "Lugu ei leitud." });

            if (formaad.ToLowerInvariant() == "nimed")
                return Ok(l.GetNoodidNimedena());

            return Ok(l.GetNoodidArvuliselt());
        }
    }
}
