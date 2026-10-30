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
        throw new NotImplementedException();
    }

    public async Task<int> AddObservationsAsync(List<Observation> observations)
    {
        throw new NotImplementedException();
    }
}