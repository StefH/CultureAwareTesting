using AwesomeAssertions;

namespace TestProject.v3;

public class UnitTests
{
    [CulturedFact(["en-US"])]
    public void CulturedFact_EN()
    {
        // Arrange
        var f = 4.01f;

        // Act
        var result = $"{f}";

        // Assert
        result.Should().Be("4.01");
    }

    [CulturedFact(["nl-NL"])]
    public void CulturedFact_NL()
    {
        // Arrange
        var f = 4.01f;

        // Act
        var result = $"{f}";

        // Assert
        result.Should().Be("4,01");
    }

    [CulturedFact(["de-DE"])]
    public void CulturedFact_DE()
    {
        // Arrange
        var f = 12345.01f;

        // Act
        var result = $"{f}";

        // Assert
        result.Should().Be("12345,01");
    }

    [CulturedTheory(["nl-NL"])]
    [InlineData(1000f, "1000")]
    [InlineData(1000.01f, "1000,01")]
    public void CulturedTheory_NL(float value, string expected)
    {
        // Act
        var result = $"{value}";

        // Assert
        result.Should().Be(expected);
    }
}