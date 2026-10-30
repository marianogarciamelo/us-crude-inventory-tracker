using CrudeTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace CrudeTracker.Data;
public class ObservationRepository
{
    private readonly CrudeTrackerDbContext _db;
    public ObservationRepository(CrudeTrackerDbContext db)
    {
        _db = db;
    }

    public async Task<HashSet<DateOnly>> GetExistingDatesAsync(string seriesCode)
    {
        return await _db.Observations
            .Where(o => o.SeriesCode == seriesCode)
            .Select(o=>o.Period) // => means for each o get the period
            .ToHashSetAsync(); //run and collect to set
    }

    public async Task<int> AddObservationsAsync(List<Observation> observations)
    {
        _db.Observations.AddRange(observations);
        return await _db.SaveChangesAsync();

    }
}