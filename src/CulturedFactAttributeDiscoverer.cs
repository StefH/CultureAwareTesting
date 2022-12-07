using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CultureAwareTesting.xUnit;

internal class CulturedFactAttributeDiscoverer : IXunitTestCaseDiscoverer
{
    private readonly IMessageSink _diagnosticMessageSink;

    public CulturedFactAttributeDiscoverer(IMessageSink diagnosticMessageSink)
    {
        _diagnosticMessageSink = diagnosticMessageSink;
    }

    public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
    {
        var ctorArgs = factAttribute.GetConstructorArguments().ToArray();
        var cultures = Reflector.ConvertArguments(ctorArgs, new[] { typeof(string[]) }).Cast<string[]>().Single();

        if (cultures == null || cultures.Length == 0)
        {
            cultures = Constants.DefaultCultures;
        }

        var methodDisplay = discoveryOptions.MethodDisplayOrDefault();
        var methodDisplayOptions = discoveryOptions.MethodDisplayOptionsOrDefault();

        return cultures.Select(culture => new CulturedXunitTestCase(_diagnosticMessageSink, methodDisplay, methodDisplayOptions, testMethod, culture)).ToList();
    }
}