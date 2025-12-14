using Microsoft.EntityFrameworkCore;
using SmashArenaAPI.Models; 
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


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
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "server=smash_db;user=root;password=rootpassword123;database=badminton_db";
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<BadmintonDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles(); 
app.UseStaticFiles();


app.UseCors("BebasAkses"); 

app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run(); 