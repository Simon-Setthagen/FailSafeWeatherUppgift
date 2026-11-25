using System.Text.Json;
using FailSafeWeatherUppgift.Data;
using FailSafeWeatherUppgift.Models;

namespace FailSafeWeatherUppgift.Services;

public class JsonImportService
{
    public void ImportWeatherData()
    {
        try
        {
            File.WriteAllText("bad_data.log", "");

            string jsonContent =
                File.ReadAllText("weather_data.json");

            var weatherRecords =
                JsonSerializer.Deserialize<List<WeatherImportRecord>>
                (jsonContent);

            int imported = 0;
            int rejected = 0;

            WeatherDbContext db = new();

            foreach (var record in weatherRecords!)
            {
                if (string.IsNullOrWhiteSpace(record.StationName))
                {
                    File.AppendAllText(
                        "bad_data.log",
                        JsonSerializer.Serialize(record)
                        + Environment.NewLine);

                    rejected++;
                    continue;
                }

                WeatherDataPoint dataPoint = new()
                {
                    StationName = record.StationName,
                    Timestamp = DateTime.Now,
                    Temperature = 0,
                    Humidity = 0,
                    DataSource = "JSON"
                };

                bool exists = db.WeatherDataPoints.Any(x => x.StationName == dataPoint.StationName);

                if (!exists)
                {
                    db.WeatherDataPoints.Add(dataPoint);
                }

                imported++;
            }

            db.SaveChanges();

            Console.WriteLine($"Imported: {imported}");
            Console.WriteLine($"Rejected: {rejected}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}