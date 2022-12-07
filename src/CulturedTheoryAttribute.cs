using Xunit;
using Xunit.Sdk;

namespace CultureAwareTesting.xUnit;

[XunitTestCaseDiscoverer("CultureAwareTesting.xUnit.CulturedTheoryAttributeDiscoverer", "CultureAwareTesting.xUnit")]
public sealed class CulturedTheoryAttribute : TheoryAttribute
{
    // ReSharper disable once UnusedParameter.Local
    public CulturedTheoryAttribute(params string[] cultures) { }
}