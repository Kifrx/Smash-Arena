using Microsoft.EntityFrameworkCore;
using SmashArenaAPI.Models; 
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. DAFTARKAN SERVICE
// ==========================================

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// CORS Config
builder.Services.AddCors(options =>
{
    options.AddPolicy("BebasAkses",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// --- PERBAIKAN 1: BACA CONNECTION STRING DARI ENV/DOCKER ---
// Jangan hardcode localhost!
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? "server=smash_db;user=root;password=rootpassword123;database=badminton_db";

var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<BadmintonDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==========================================
// 2. ATUR PIPELINE
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Matikan HTTPS Redirection agar tidak loop di Cloudflare Flexible
// app.UseHttpsRedirection(); 

app.UseCors("BebasAkses");

// --- PERBAIKAN 2: AKTIFKAN WEBSITE STATIS (HTML) ---
app.UseDefaultFiles(); // Agar otomatis buka index.html
app.UseStaticFiles();  // Agar bisa baca css/js/gambar
// ---------------------------------------------------

app.UseAuthorization();

app.MapControllers();

// Fallback: Jika user buka alamat aneh, kembalikan ke index.html (SPA Style)
app.MapFallbackToFile("index.html");

app.Run();