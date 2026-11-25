namespace FailSafeWeatherUppgift.Models;

public class WeatherDataPoint
{
    public int Id { get; set; }
    public string StationName { get; set; } = string.Empty;

    public string DataSource { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }

}