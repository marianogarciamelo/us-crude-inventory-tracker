using CrudeTracker.Core;

namespace CrudeTracker.Core.Tests;

public class ObservationValidatorTests
{
    [Fact]
    public void Validate_NegativeValue_Throws()
    {
        // Arrange: build a bad observation on purpose
        var bad = new Observation("WCESTUS1", new DateOnly(2026, 10, 23), -5m);

        // Act + Assert: calling Validate should throw
        Assert.Throws<InvalidDataException>(
            () => ObservationValidator.Validate(bad, "WCESTUS1"));
    }

    // TODO : Validate_WrongSeries_Throws
    [Fact]
    public void Validate_WrongSeries_Throws()
    {
        var bad = new Observation("WRONGSERIES", new DateOnly(2026, 10, 23), 5m);

        Assert.Throws<InvalidDataException>(
            () => ObservationValidator.Validate(bad, "WCESTUS1")); //lambda function a fuynction with no name passed in to our throw
    }
    // TODO : Validate_ValidObservation_DoesNotThrow
    [Fact]
    public void Validate_ValidObservation_DoesNotThrow()
    {
        var good = new Observation("WCESTUS1", new DateOnly(2026, 10, 23), 10m);

        var ex = Record.Exception(() => ObservationValidator.Validate(good, "WCESTUS1")); //records if an exception is returned
        
        Assert.Null(ex); //check if it is null
    }
}