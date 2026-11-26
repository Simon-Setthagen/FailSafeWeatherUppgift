using System.Net.Http;

namespace FailSafeWeatherUppgift.Services;

public class WeatherApiService
{
    public void FetchWeatherData()
    {
        try
        {
            HttpClient client = new();

            string url = "https://api.open-meteo.com/v1/forecast?latitude=57.6832&longitude=11.9511&hourly=temperature_2m,relative_humidity_2m";

            string result =client.GetStringAsync(url).Result;

            Console.WriteLine("API request successful.");
        }
        catch
        {
            Console.WriteLine("API request failed.");
        }
    }
}
