# FailSafe Weather Monitor

## Beskrivning

FailSafe Weather Monitor är en konsolapplikation utvecklad i C\# med Entity Framework Core. Programmet läser in väderdata från en JSON\-fil, lagrar informationen i en SQL Server\-databas och kan hämta väderdata från ett externt API.

Projektet är utvecklat som en del av kursens inlämningsuppgift med fokus på databasutveckling, Entity Framework Core, felhantering och grundläggande DevSecOps\-principer.

## Funktioner

- Importerar väderdata från JSON\-fil
- Sparar data i databas med Entity Framework Core
- Loggar felaktiga poster till bad\_data.log
- Hämtar väderdata från Open\-Meteo API
- Visar statistik över sparad data
- Konsolmeny byggd med Spectre.Console
- Använder EF Core Code First och Migrations
- Förhindrar dubbletter genom kontroll av StationName och Timestamp

## Tekniker

- C\#
- .NET 11
- Entity Framework Core
- SQL Server LocalDB
- Spectre.Console
- Open\-Meteo API

## Systemkrav

För att köra projektet krävs:

- .NET 11 SDK
- Visual Studio 2022 eller senare
- SQL Server LocalDB

Kontrollera installerad .NET\-version:

dotnet \-\-version

Projektet är utvecklat för:

.NET 11.0

## Databas

Projektet använder Entity Framework Core Code First.

Databasen skapas med hjälp av migrations:

Add\-Migration InitialCreate

Update\-Database

Datamodellen består av följande fält:

- Id
- StationName
- Timestamp
- Temperature
- Humidity
- DataSource

## Menyalternativ

### Import Weather Data

Läser in data från weather\_data.json, validerar poster, filtrerar bort felaktiga värden och loggar ogiltiga rader till bad\_data.log.

### Fetch API Data

Hämtar väderdata från Open\-Meteo API med hjälp av HttpClient.

### Show Statistics

Visar:

- Totalt antal poster
- Högsta temperatur
- Lägsta temperatur
- Medeltemperatur

### Exit

Avslutar programmet.

## Validering

Programmet använder TryParse() för att validera importerad data.

Följande kontroller utförs:

- Temperatur måste kunna konverteras till numeriskt värde
- Luftfuktighet måste kunna konverteras till numeriskt värde
- Tidsstämpel måste kunna konverteras till DateTime
- StationName får inte vara tom

Felaktiga poster:

- Loggas till bad\_data.log
- Sparas inte i databasen
- Får inte krascha applikationen

## Dubbletthantering

Innan en datapunkt sparas i databasen kontrollerar programmet om samma:

- StationName
- Timestamp

redan existerar i databasen.

Om datapunkten redan finns lagras den inte igen.

## Felhantering

Programmet använder try\-catch vid:

- Filinläsning
- JSON\-hantering
- API\-anrop

Felaktig data loggas till:

bad\_data.log

för att förhindra att programmet kraschar.

## Projektstruktur

FailSafeWeatherUppgift

Data

WeatherDbContext.cs

 Models

WeatherDataPoint.cs

WeatherImportRecord.cs

 Services

JsonImportService.cs

WeatherApiService.cs

Migrations

Program.cs

 appsettings.json

 weather\_data.json

 README.md

## Installation

1. Klona projektet från GitHub.
2. Säkerställ att .NET 11 SDK är installerad.
3. Öppna lösningen i Visual Studio.
4. Kör migrationerna:

Update\-Database

1. Starta programmet.
2. Välj önskad funktion från menyn.

## Exempel på funktioner

Programmet kan:

- Importera väderdata från JSON\-fil
- Spara data till SQL Server\-databas
- Hämta väderdata från API
- Visa statistik över sparad data
- Logga felaktiga poster
- Förhindra dubbletter

