using AeroManag.Core.Interfaces;
using AeroManag.Core.Services;
using AeroManag.Infrastructure.Data;
using AeroManag.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' introuvable.");
builder.Services.AddSingleton(new SqliteConnectionFactory(connectionString));

builder.Services.AddScoped<IAeroportRepository, AeroportRepository>();
builder.Services.AddScoped<IAeroportService, AeroportService>();
builder.Services.AddScoped<IAvionRepository, AvionRepository>();
builder.Services.AddScoped<IAvionService, AvionService>();
builder.Services.AddScoped<IPersonnelRepository, PersonnelRepository>();
builder.Services.AddScoped<IPersonnelService, PersonnelService>();
builder.Services.AddScoped<IPassagerRepository, PassagerRepository>();
builder.Services.AddScoped<IPassagerService, PassagerService>();
builder.Services.AddScoped<IVolRepository, VolRepository>();
builder.Services.AddScoped<IVolService, VolService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

InitializeDatabase(connectionString);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();

app.Run();

void InitializeDatabase(string connStr)
{
    var dbPath = connStr.Replace("Data Source=", "").Trim();
    if (File.Exists(dbPath)) return;

    var scriptPath = Path.Combine(AppContext.BaseDirectory, "database.sql");
    var script = File.ReadAllText(scriptPath);

    using var connection = new SqliteConnection(connStr);
    connection.Open();
    var instructions = script.Split(';', StringSplitOptions.RemoveEmptyEntries);
    foreach (var instruction in instructions)
    {
        var sql = instruction.Trim();
        if (string.IsNullOrWhiteSpace(sql)) continue;
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
