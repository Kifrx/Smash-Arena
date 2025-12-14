using Microsoft.EntityFrameworkCore;
using SmashArenaAPI.Models; 
using System.Text.Json.Serialization; // <--- Wajib ada

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. DAFTARKAN SERVICE (Dapur)
// ==========================================

// Fix Circular Reference (Obat Pusing)
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Konfigurasi CORS (Biar Frontend bisa masuk)
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

// Database MySQL 
var connectionString = "server=localhost;user=root;password=;database=badminton_db";
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<BadmintonDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==========================================
// 2. ATUR PIPELINE (Pelayan)
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("BebasAkses"); // Pasang CORS

app.UseAuthorization();

app.MapControllers();

app.Run(); // <--- Cukup satu kali saja di paling akhir!