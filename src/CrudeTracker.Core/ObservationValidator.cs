namespace CrudeTracker.Core;

public class ObservationValidator
{
    public static void Validate(Observation obs, string expectedSeries)
    {
        if (obs.Value < 0)
        {
            throw new InvalidDataException($"Negative value {obs.Value} for {obs.SeriesCode} on {obs.Period}.");
        }

        if (obs.SeriesCode != expectedSeries)
        {
            throw new InvalidDataException($"Expected series {expectedSeries} but got {obs.SeriesCode} on {obs.Period}.");
        }
    }
}