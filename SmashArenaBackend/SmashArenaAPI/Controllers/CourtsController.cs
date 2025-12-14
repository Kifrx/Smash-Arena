using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmashArenaAPI.Models;

namespace SmashArenaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtsController : ControllerBase
    {
        private readonly BadmintonDbContext _context;

        public CourtsController(BadmintonDbContext context)
        {
            _context = context;
        }

        // GET: api/Courts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Court>>> GetCourts()
        {
            return await _context.Courts.ToListAsync();
        }

        // GET: api/Courts
        [HttpGet("{id}")]
        public async Task<ActionResult<Court>> GetCourt(string id)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null) return NotFound();
            return court;
        }

        // POST: api/Courts
        [HttpPost]
        public async Task<ActionResult<Court>> PostCourt([FromBody] CourtRequest request)
        {

            string newId = "C" + new Random().Next(1000, 9999).ToString();
            var newCourt = new Court
            {
                CourtId = newId,
                NamaCourt = request.NamaCourt,
                JenisLantai = request.JenisLantai,
                Fasilitas = request.Fasilitas,
                HargaPerJam = request.HargaPerJam,
                GambarUrl = request.GambarUrl,
                StatusAktif = true
            };

            _context.Courts.Add(newCourt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourt", new { id = newCourt.CourtId }, newCourt);
        }

        // PUT: api/Courts
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourt(string id, [FromBody] CourtRequest request)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null) return NotFound();

            court.NamaCourt = request.NamaCourt;
            court.JenisLantai = request.JenisLantai;
            court.Fasilitas = request.Fasilitas;
            court.HargaPerJam = request.HargaPerJam;
            court.GambarUrl = request.GambarUrl;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Data lapangan berhasil diupdate!" });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourt(string id)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null) return NotFound();

            _context.Courts.Remove(court);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Lapangan berhasil dihapus!" });
        }
    }

    public class CourtRequest
    {
        public string NamaCourt { get; set; }
        public string JenisLantai { get; set; }
        public string Fasilitas { get; set; }
        public decimal HargaPerJam { get; set; }
        public string GambarUrl { get; set; }
        public bool? StatusAktif { get; set; }
    }
}