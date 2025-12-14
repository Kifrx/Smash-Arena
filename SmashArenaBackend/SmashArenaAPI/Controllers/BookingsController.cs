using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmashArenaAPI.Models;

namespace SmashArenaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BadmintonDbContext _context;

        public BookingsController(BadmintonDbContext context)
        {
            _context = context;
        }

        // BOOKING LAPANGAN
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(BookingRequest request)
        {
            // 1. cek jam valid (Buka 08:00 - 18:00)
            if (request.JamMulai < new TimeOnly(8, 0) || request.JamMulai >= new TimeOnly(18, 0))
            {
                return BadRequest(new { message = "Maaf, GOR hanya buka jam 08:00 - 18:00." });
            }

            // 2.  cek lapangan sudah dibook?
            var bentrok = await _context.Bookings
                .AnyAsync(b => b.CourtId == request.CourtId 
                            && b.TanggalMain == request.TanggalMain 
                            && b.JamMulai == request.JamMulai
                            && b.StatusBooking != "Cancelled");

            if (bentrok)
            {
                return Conflict(new { message = "Yah, Lapangan sudah dibooking orang lain di jam segitu!" });
            }

            // 3. Ambil Harga Lapangan
            var court = await _context.Courts.FindAsync(request.CourtId);
            if(court == null) return NotFound(new { message = "Lapangan tidak ditemukan" });

            // 4. Buat Data Booking Baru
            var newBooking = new Booking
            {
                BookingId = "B" + DateTime.Now.ToString("MMddHHmmss"),
                UserId = request.UserId,
                CourtId = request.CourtId,
                TanggalMain = request.TanggalMain,
                JamMulai = request.JamMulai,
                JamSelesai = request.JamMulai.AddHours(1),
                TotalHarga = court.HargaPerJam,
                StatusBooking = "Confirmed",
                WaktuDibuat = DateTime.Now
            };

            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking Berhasil!", bookingId = newBooking.BookingId });
        } 

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetMyBookings(string userId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Court) 
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.WaktuDibuat)
                .ToListAsync();

            if (bookings == null)
            {
                return NotFound();
            }

            return bookings;
        }


        // Admin: LIHAT SEMUA DAFTAR BOOKING
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            return await _context.Bookings
                .Include(b => b.Court) 
                .Include(b => b.User)  
                .OrderByDescending(b => b.WaktuDibuat) 
                .ToListAsync();
        }

        [HttpGet("check")]
        public async Task<ActionResult<IEnumerable<string>>> CheckAvailability(string courtId, DateOnly date)
        {
            var bookedTimes = await _context.Bookings
                .Where(b => b.CourtId == courtId 
                            && b.TanggalMain == date 
                            && b.StatusBooking != "Cancelled") 
                .Select(b => b.JamMulai)
                .ToListAsync();

            return bookedTimes.Select(t => t.Value.ToString("HH:mm")).ToList();
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(string id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound(new { message = "Booking tidak ditemukan" });
            }

            booking.StatusBooking = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking berhasil dibatalkan!" });
        }

    } 
    public class BookingRequest
    {
        public string UserId { get; set; }
        public string CourtId { get; set; }
        public DateOnly TanggalMain { get; set; }
        public TimeOnly JamMulai { get; set; }
    }
}