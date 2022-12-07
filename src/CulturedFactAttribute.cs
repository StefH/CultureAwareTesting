using Xunit;
using Xunit.Sdk;

namespace CultureAwareTesting.xUnit;

[XunitTestCaseDiscoverer("CultureAwareTesting.xUnit.CulturedFactAttributeDiscoverer", "CultureAwareTesting.xUnit")]
public sealed class CulturedFactAttribute : FactAttribute
{
    // ReSharper disable once UnusedParameter.Local
    public CulturedFactAttribute(params string[] cultures) { }
}