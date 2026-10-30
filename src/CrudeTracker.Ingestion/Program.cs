using CrudeTracker.Ingestion;
using CrudeTracker.Data;
using Microsoft.EntityFrameworkCore;

var apiKey = Environment.GetEnvironmentVariable("EIA_API_KEY");
if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("EIA_API_KEY not set. Run: source ~/.zshrc");
    return;
}

var connectionString = Environment.GetEnvironmentVariable("CRUDETRACKER_DB");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("CRUDETRACKER_DB not set. Run: source ~/.zshrc");
    return;
}

var client = new EiaClient(apiKey);

var observations = await client.FetchAsync("WCESTUS1");

var options = new DbContextOptionsBuilder<CrudeTrackerDbContext>()
    .UseNpgsql(connectionString)
    .UseSnakeCaseNamingConvention()
    .Options;

await using var db = new CrudeTrackerDbContext(options);
var repo = new ObservationRepository(db);

var existing = await repo.GetExistingDatesAsync("WCESTUS1");

var newOnes = observations
    .Where(o => !existing.Contains(o.Period))
    .ToList();

var inserted = await repo.AddObservationsAsync(newOnes);


