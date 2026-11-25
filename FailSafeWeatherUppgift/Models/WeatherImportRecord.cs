namespace FailSafeWeatherUppgift.Models;

public class WeatherImportRecord
{
    public string? StationName { get; set; }

    public string? Timestamp { get; set; }

    public object? Temperature { get; set; }

    public object? Humidity { get; set; }
}