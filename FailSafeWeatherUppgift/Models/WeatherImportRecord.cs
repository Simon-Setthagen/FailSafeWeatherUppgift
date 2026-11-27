using System.ComponentModel.DataAnnotations;

namespace FailSafeWeatherUppgift.Models;
public class WeatherImportRecord
{
    [Required]
    [MaxLength(100)]
    public string? StationName { get; set; }
    public string? Timestamp { get; set; }
    public object? Temperature { get; set; }
    public object? Humidity { get; set; }
}