using CrudeTracker.Core;

namespace CrudeTracker.Core.Tests;

public class ChangeCalculatorTests
{
    // Shared sample data, deliberately unsorted
    private static List<Observation> Sample() =>
    [
        new("WCESTUS1", new DateOnly(2026, 10, 2),  424134m),
        new("WCESTUS1", new DateOnly(2026, 10, 16), 412517m),
        new("WCESTUS1", new DateOnly(2026, 9, 18),  426398m),
        new("WCESTUS1", new DateOnly(2026, 10, 9),  415577m),
        new("WCESTUS1", new DateOnly(2026, 9, 25),  427320m),
    ];

    [Fact]
    public void Compute_EmptyList_ReturnsEmpty()
    {
        var result = ChangeCalculator.Compute(new List<Observation>());

        Assert.Empty(result);
    }

    [Fact]
    public void Compute_SingleObservation_ReturnsEmpty()
    {
        var observations = new List<Observation>
        {
            new("WCESTUS1", new DateOnly(2026, 10, 16), 412517m),
        };

        var result = ChangeCalculator.Compute(observations);

        Assert.Empty(result);
    }

    [Fact]
    public void Compute_FiveObservations_ReturnsFourChanges()
    {
        var result = ChangeCalculator.Compute(Sample());

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Compute_UnsortedInput_FirstResultIsOldestChange()
    {
        var result = ChangeCalculator.Compute(Sample());

        Assert.Equal(new DateOnly(2026, 9, 25), result[0].Period);
        Assert.Equal(922m, result[0].Change);
    }

    [Fact]
    public void Compute_UnsortedInput_IncludesNewestWeek()
    {
        // Regression test: an off-by-one loop bound once dropped the newest week
        var result = ChangeCalculator.Compute(Sample());

        Assert.Equal(new DateOnly(2026, 10, 16), result[^1].Period);
        Assert.Equal(-3060m, result[^1].Change);
    }
}