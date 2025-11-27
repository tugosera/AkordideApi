using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkordideApi.Data;
using AkordideApi.Models;

namespace AkordideApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KolmkoladController : ControllerBase
    {
        private readonly Data.AkordContext _context;

        public KolmkoladController(Data.AkordContext context)
        {
            _context = context;
        }

        // GET: api/kolmkolad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kolmkola>>> GetAll()
        {
            return await _context.Kolmkolad.ToListAsync();
        }

        // GET: api/kolmkolad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kolmkola>> Get(int id)
        {
            var k = await _context.Kolmkolad.FindAsync(id);
            if (k == null) return NotFound(new { message = "Kolmkõla ei leitud." });
            return k;
        }

        // POST: api/kolmkolad
        // Body: { "Tahis": "C" } or { "Pohitoon": 60 }
        [HttpPost]
        public async Task<ActionResult<Kolmkola>> Create([FromBody] Kolmkola input)
        {
            // Kui antud on Tahis, leiame põhitooni
            if (!string.IsNullOrEmpty(input.Tahis) && input.Pohitoon == 0)
            {
                input.Pohitoon = Kolmkola.NameToMidi(input.Tahis);
            }

            _context.Kolmkolad.Add(input);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = input.Id }, input);
        }

        // PUT: api/kolmkolad/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Kolmkola updated)
        {
            var existing = await _context.Kolmkolad.FindAsync(id);
            if (existing == null) return NotFound(new { message = "Kolmkõla ei leitud." });

            existing.Pohitoon = updated.Pohitoon;
            existing.Tahis = updated.Tahis ?? Kolmkola.MidiToName(updated.Pohitoon);

            _context.Kolmkolad.Update(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var kolmkola = await _context.Kolmkolad
                .Include(k => k.Taktid)
                .FirstOrDefaultAsync(k => k.Id == id);

            if (kolmkola == null)
                return NotFound();

            if (kolmkola.Taktid.Any())
                return BadRequest("Нельзя удалить аккорд, потому что существуют такты, связанные с ним.");

            _context.Kolmkolad.Remove(kolmkola);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET: api/kolmkolad/5/noodid
        [HttpGet("{id}/noodid")]
        public async Task<ActionResult> GetNoodid(int id, [FromQuery] string formaad = "arv")
        {
            var k = await _context.Kolmkolad.FindAsync(id);
            if (k == null) return NotFound(new { message = "Kolmkõla ei leitud." });

            if (formaad.ToLowerInvariant() == "nimed")
                return Ok(k.GetNimed());
            return Ok(k.GetNoodid());
        }
    }
}
