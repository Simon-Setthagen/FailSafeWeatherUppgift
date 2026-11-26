using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using FailSafeWeatherUppgift.Models;

namespace FailSafeWeatherUppgift.Data;

public class WeatherDbContext : DbContext
{
    public DbSet<WeatherDataPoint> WeatherDataPoints { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string path = Path.Combine(
                AppContext.BaseDirectory, "appsettings.json");

            string json = File.ReadAllText(path);

            using JsonDocument document = JsonDocument.Parse(json);

            string connectionString = document.RootElement
                .GetProperty("ConnectionStrings")
                .GetProperty("DefaultConnection")
                .GetString()
                ?? throw new InvalidOperationException(
                    "Connection string is missing.");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
