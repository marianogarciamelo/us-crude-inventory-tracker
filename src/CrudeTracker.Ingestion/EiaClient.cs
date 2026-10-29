using CrudeTracker.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CrudeTracker.Ingestion;

public class EiaClient
{
    private readonly string _apiKey;
    public EiaClient(string apiKey)
    {
        _apiKey = apiKey;

        if (string.IsNullOrEmpty(_apiKey)) {
            throw new ArgumentException("EIA_API_KEY key is required");
        }
    }

    public async Task<List<Observation>> FetchAsync(string seriesCode)
    {
        List<Observation> dataIngest = new List<Observation>();

        var url = "https://api.eia.gov/v2/petroleum/stoc/wstk/data/"
        + $"?api_key={_apiKey}"
        + "&frequency=weekly"
        + "&data[0]=value"
        + $"&facets[series][]={seriesCode}"
        + "&sort[0][column]=period&sort[0][direction]=desc"
        + "&length=5000";

        using var http = new HttpClient();
        var json = await http.GetStringAsync(url);
        var options = new JsonSerializerOptions 
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        var resultOfFetch = JsonSerializer.Deserialize<EiaResponse>(json, options);
        var rows = GetRows(resultOfFetch);

        foreach (var row in rows)
        {
            var observationToAdd = new Observation(row.Series, DateOnly.Parse(row.Period), row.Value);
            ObservationValidator.Validate(observationToAdd, seriesCode);
            dataIngest.Add(observationToAdd);

        }
        return dataIngest;
    }

    private static List<EiaRow> GetRows(EiaResponse? response)
    {
        if (response?.Response?.Data is null)
        {
            throw new InvalidDataException("EIA response had no data. Check the API key and url.");
        }
        if (response.Response.Data.Count == 0)
        {
            throw new InvalidDataException("EIA returned zero rows.");
        }
        return response.Response.Data;
    }

}