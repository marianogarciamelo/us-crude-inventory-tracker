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
    // TODO : Validate_ValidObservation_DoesNotThrow
}