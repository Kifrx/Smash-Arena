using Microsoft.EntityFrameworkCore;
using SmashArenaAPI.Models; 
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? "server=smash_db;user=root;password=rootpassword123;database=badminton_db";

var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<BadmintonDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<BadmintonDbContext>();
        // Perintah ini akan membuat database & tabel otomatis jika belum ada
        dbContext.Database.EnsureCreated(); 
        Console.WriteLine("✅ Database & Tabel berhasil dibuat/dipastikan ada!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Gagal membuat database: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("BebasAkses");
app.UseDefaultFiles(); 
app.UseStaticFiles(); 

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();