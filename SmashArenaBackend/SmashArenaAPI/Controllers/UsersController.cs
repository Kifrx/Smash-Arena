using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmashArenaAPI.Models;

namespace SmashArenaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BadmintonDbContext _context;

        public UsersController(BadmintonDbContext context)
        {
            _context = context;
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Email atau Password salah!" });
            }

            return Ok(user);
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            Console.WriteLine($"Mencoba daftar: Nama={request.NamaLengkap}, Email={request.Email}");

            // 1. Cek apakah Email sudah ada
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                Console.WriteLine("Gagal: Email sudah ada di DB.");
                return BadRequest(new { message = "Email sudah terdaftar!" });
            }

            // 2. Buat User Baru 
            var newUser = new User
            {
                UserId = "U" + new Random().Next(1000, 9999).ToString(),
                NamaLengkap = request.NamaLengkap,
                Email = request.Email,
                Password = request.Password,
                NomorHp = request.NomorHp,
                Role = "User" 
            };
            
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            Console.WriteLine("Sukses: User baru tersimpan.");
            return Ok(new { message = "Registrasi Berhasil!" });
        }
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NomorHp { get; set; }
    }
}