using CrudeTracker.Ingestion;

var apiKey = Environment.GetEnvironmentVariable("EIA_API_KEY");
if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("EIA_API_KEY not set. Run: source ~/.zshrc");
    return;
}

var client = new EiaClient(apiKey);

var observations = await client.FetchAsync("WCESTUS1");

foreach (var obs in observations)
{
    Console.WriteLine(obs);
}