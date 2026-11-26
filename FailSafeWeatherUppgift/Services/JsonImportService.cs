using System.Globalization;
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
            string jsonContent = File.ReadAllText("weather_data.json");

            var weatherRecords =
                JsonSerializer.Deserialize<List<JsonElement>>(jsonContent);

            if (weatherRecords == null)
            {
                Console.WriteLine("No weather data found.");
                return;
            }

            int imported = 0;
            int rejected = 0;
            int duplicates = 0;

            using WeatherDbContext db = new();

            foreach (var row in weatherRecords)
            {
                WeatherImportRecord? record;

                try
                {
                    record = row.Deserialize<WeatherImportRecord>();
                }
                catch (JsonException)
                {
                    SaveError(row.ToString(), "Invalid JSON record");
                    rejected++;
                    continue;
                }
                if (record == null ||
                    string.IsNullOrWhiteSpace(record.StationName))
                {
                    SaveError(row.ToString(), "Missing station name");
                    rejected++;
                    continue;
                }

                if (!DateTime.TryParseExact(
                    record.Timestamp,
                    "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime timestamp))
                {
                    SaveError(row.ToString(), "Invalid timestamp format");
                    rejected++;
                    continue;
                }

                if (!double.TryParse(
                    record.Temperature?.ToString(),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double temperature))
                {
                    SaveError(row.ToString(), "Invalid temperature");
                    rejected++;
                    continue;
                }

                if (!double.TryParse(
                    record.Humidity?.ToString(),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double humidity))
                {
                    SaveError(row.ToString(), "Invalid humidity");
                    rejected++;
                    continue;
                }

                if (!double.IsFinite(temperature)||
                    temperature < -60 || temperature > 60)
                {
                    SaveError(row.ToString(), "Temperature out of range");
                    rejected++;
                    continue;
                }

                if (!double.IsFinite(humidity) ||
                    humidity < 0 || humidity > 100)
                {
                    SaveError(row.ToString(), "Humidity out of range");
                    rejected++;
                    continue;
                }
                bool exists = db.WeatherDataPoints.Any(x =>
                                    x.StationName == record.StationName &&
                                    x.Timestamp == timestamp);

                if (exists)
                {
                    duplicates++;
                    continue;
                }

                WeatherDataPoint dataPoint = new()
                {
                    StationName = record.StationName,
                    Timestamp = timestamp,
                    Temperature = temperature,
                    Humidity = humidity,
                    DataSource = "JSON"
                };

                db.WeatherDataPoints.Add(dataPoint);
                db.SaveChanges();

                imported++;
            }

            Console.WriteLine($"Imported: {imported}");
            Console.WriteLine($"Rejected: {rejected}");
            Console.WriteLine($"Duplicates: {duplicates}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Import failed: {ex.Message}");
        }
    }

    private void SaveError(string record, string reason)
    {
        File.AppendAllText(
            "bad_data.log",
            $"{DateTime.Now} | {reason} | {record}" +
            Environment.NewLine);
    }
}