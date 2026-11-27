using Spectre.Console;
using FailSafeWeatherUppgift.Services;
using FailSafeWeatherUppgift.Data;

WeatherDbContext db = new();
JsonImportService importService = new();
WeatherApiService apiService = new();

bool running = true;

while (running)
{
    string choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("FailSafe Weather Monitor")
                .AddChoices(
                    "Import Weather Data",
                    "Fetch API Data",
                    "Show Statistics",
                    "Exit"));

    switch (choice)
    {
        case "Import Weather Data":
            importService.ImportWeatherData();
            break;

        case "Fetch API Data":
            apiService.GetWeatherData();
            break;

        case "Show Statistics":
            Console.WriteLine($"Total records: {db.WeatherDataPoints.Count()}");
            Console.WriteLine($"Highest temperature: {db.WeatherDataPoints.Max(x => x.Temperature)}");
            Console.WriteLine($"Lowest temperature: {db.WeatherDataPoints.Min(x => x.Temperature)}");
            Console.WriteLine($"Average temperature: {db.WeatherDataPoints.Average(x => x.Temperature)}");
            break;

        case "Exit":
            running = false;
            break;
    }
}