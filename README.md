# FailSafe Weather Monitor

## Beskrivning

FailSafe Weather Monitor är en konsolapplikation utvecklad i C# med Entity Framework Core. Programmet läser in väderdata från en JSON-fil, lagrar informationen i en SQL Server-databas och kan hämta väderdata från ett externt API.

Projektet är utvecklat som en del av kursens inlämningsuppgift med fokus på databasutveckling, EF Core, felhantering och grundläggande DevSecOps-principer.

## Funktioner

- Importerar väderdata från JSON-fil
- Sparar data i databas med Entity Framework Core
- Loggar felaktiga poster till `bad_data.log`
- Hämtar väderdata från Open-Meteo API
- Visar statistik över sparad data
- Konsolmeny byggd med Spectre.Console
- Använder EF Core Code First och Migrations

## Tekniker

- C#
- .NET
- Entity Framework Core
- SQL Server LocalDB
- Spectre.Console
- Open-Meteo API

## Databas

Projektet använder Entity Framework Core Code First.

Tabellen skapas med hjälp av migrations:

```powershell
Add-Migration InitialCreate
Update-Database
```

Datamodellen består av följande fält:

- Id
- StationName
- Timestamp
- Temperature
- Humidity
- DataSource

## Menyalternativ

Programmet innehåller följande funktioner:

### Import Weather Data

Läser in data från `weather_data.json`, validerar poster och loggar felaktiga rader till `bad_data.log`.

### Fetch API Data

Hämtar väderdata från Open-Meteo API med hjälp av HttpClient.

### Show Statistics

Visar:

- Totalt antal poster
- Högsta temperatur
- Lägsta temperatur
- Medeltemperatur

### Exit

Avslutar programmet.

## Felhantering

Programmet använder try-catch vid:

- Filinläsning
- API-anrop

Felaktiga poster loggas i filen:

```text
bad_data.log
```

för att programmet inte ska krascha vid ogiltig data.

## Projektstruktur

```text
FailSafeWeatherUppgift
│
├── Data
│   └── WeatherDbContext.cs
│
├── Models
│   ├── WeatherDataPoint.cs
│   └── WeatherImportRecord.cs
│
├── Services
│   ├── JsonImportService.cs
│   └── WeatherApiService.cs
│
├── Migrations
│
├── Program.cs
├── appsettings.json
├── weather_data.json
└── README.md
```

## Installation

1. Klona projektet från GitHub.
2. Öppna lösningen i Visual Studio.
3. Kör migrationerna:

```powershell
Update-Database
```

4. Starta programmet.

## Författare

Simon Setthagen
