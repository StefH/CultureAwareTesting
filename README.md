# CultureAwareTesting
CultureAwareTesting extensions for [xUnit](https://github.com/xunit/xunit).

## Overview
This project is based on [test.utility/CultureAwareTesting(]https://github.com/xunit/xunit/tree/master/test/test.utility/CultureAwareTesting) and supports 2 attributes which can be used to define the Culture for a specific test:
- `CulturedFact` in addition to the normal `Fact`
- `CulturedTheory` in addition to the normal `Theory`

## NuGet
[![NuGet Badge](https://buildstats.info/nuget/CultureAwareTesting.xUnit)](https://www.nuget.org/packages/CultureAwareTesting.xUnit)

## Example

``` c#
public class UnitTests
{
    [CulturedFact("en-US")]
    public void CulturedFact_EN()
    {
        // Arrange
        var f = 4.01f;

        // Act
        var result = $"{f}";

        // Assert
        result.Should().Be("4.01");
    }

    [CulturedFact("nl-NL")]
    public void CulturedFact_NL()
    {
        // Arrange
        var f = 4.01f;

        // Act
        var result = $"{f}";

        // Assert
        result.Should().Be("4,01");
    }

    [CulturedFact("de-DE")]
    public void CulturedFact_DE()
    {
        // Arrange
        var f = 12345.01f;

        // Act
        var result = $"{f}";

        // Assert
        result.Should().Be("12345,01");
    }

    [CulturedTheory("nl-NL")]
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
```
