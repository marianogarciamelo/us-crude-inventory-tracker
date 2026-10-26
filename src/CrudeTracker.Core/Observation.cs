namespace CrudeTracker.Core;

public record Observation(string SeriesCode, DateOnly Period, decimal Value);