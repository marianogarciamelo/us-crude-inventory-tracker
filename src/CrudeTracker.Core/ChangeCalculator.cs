namespace CrudeTracker.Core;

public record WeeklyChange(DateOnly Period, decimal Value, decimal Change);

public static class ChangeCalculator
{
    public static List<WeeklyChange> Compute(IReadOnlyList<Observation> observations)
    {
        var sortedObservationsList = observations.OrderBy(o => o.Period).ToList();
        var results = new List<WeeklyChange>();

        for (var i = 1; i < sortedObservationsList.Count; i++)
        {
            var currentWeek = sortedObservationsList[i];
            var previousWeek = sortedObservationsList[i - 1];
            var change = currentWeek.Value - previousWeek.Value;
            results.Add(new WeeklyChange(currentWeek.Period, currentWeek.Value, change));
        }
        return results;
    }
}