# ![logo](./resources/logo_32x32.png) CultureAwareTesting
CultureAwareTesting extensions for [xUnit](https://github.com/xunit/xunit).

## Overview
This project is based on [test.utility/CultureAwareTesting](https://github.com/xunit/xunit/tree/master/test/test.utility/CultureAwareTesting) and supports 2 attributes which can be used to define the Culture for a specific test:
- `CulturedFact` in addition to the normal `Fact`
- `CulturedTheory` in addition to the normal `Theory`

## NuGet
[![NuGet Badge](https://img.shields.io/nuget/v/CultureAwareTesting.xUnit)](https://www.nuget.org/packages/CultureAwareTesting.xUnit)

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

## Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=StefH) and [Dapper Plus](https://dapper-plus.net/?utm_source=StefH) are major sponsors and proud to contribute to the development of **CultureAwareTesting.xUnit**.

[![Entity Framework Extensions](https://raw.githubusercontent.com/StefH/resources/main/sponsor/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert?utm_source=StefH)

[![Dapper Plus](https://raw.githubusercontent.com/StefH/resources/main/sponsor/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert?utm_source=StefH)