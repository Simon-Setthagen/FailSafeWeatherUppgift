using System.ComponentModel.DataAnnotations;

namespace FailSafeWeatherUppgift.Models;
public class WeatherDataPoint
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string StationName { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string DataSource { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }

}