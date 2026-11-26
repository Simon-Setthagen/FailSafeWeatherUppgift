using Microsoft.EntityFrameworkCore;
using FailSafeWeatherUppgift.Models;
namespace FailSafeWeatherUppgift.Data;
public class WeatherDbContext : DbContext
{
    public DbSet<WeatherDataPoint> WeatherDataPoints { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WeatherMonitorDb;Trusted_Connection=True;");
    }
}