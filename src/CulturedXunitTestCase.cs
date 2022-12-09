using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CultureAwareTesting.xUnit;

internal class CulturedXunitTestCase : XunitTestCase
{
    private string _culture = null!;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
    // ReSharper disable once UnusedMember.Global
    public CulturedXunitTestCase() { }

    public CulturedXunitTestCase(IMessageSink diagnosticMessageSink,
        TestMethodDisplay defaultMethodDisplay,
        TestMethodDisplayOptions defaultMethodDisplayOptions,
        ITestMethod testMethod,
        string culture,
        object[]? testMethodArguments = null)
        : base(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod, testMethodArguments)
    {
        Initialize(culture);
    }

    private void Initialize(string culture)
    {
        _culture = culture;

        Traits.Add(Constants.TraitKeyCulture, new List<string> { culture });

        DisplayName += $"[{culture}]";
    }

    protected override string GetUniqueID()
        => $"{base.GetUniqueID()}[{_culture}]";

    public override void Deserialize(IXunitSerializationInfo data)
    {
        base.Deserialize(data);

        Initialize(data.GetValue<string>(Constants.TraitKeyCulture));
    }

    public override void Serialize(IXunitSerializationInfo data)
    {
        base.Serialize(data);

        data.AddValue(Constants.TraitKeyCulture, _culture);
    }

    public override async Task<RunSummary> RunAsync(
        IMessageSink diagnosticMessageSink,
        IMessageBus messageBus,
        object[] constructorArguments,
        ExceptionAggregator aggregator,
        CancellationTokenSource cancellationTokenSource)
    {
        var originalCulture = CurrentCulture;
        var originalUICulture = CurrentUICulture;

        try
        {
            var cultureInfo = new CultureInfo(_culture);
            CurrentCulture = cultureInfo;
            CurrentUICulture = cultureInfo;

            return await base.RunAsync(diagnosticMessageSink, messageBus, constructorArguments, aggregator, cancellationTokenSource);
        }
        finally
        {
            CurrentCulture = originalCulture;
            CurrentUICulture = originalUICulture;
        }
    }

    private static CultureInfo CurrentCulture
    {
#if NETFRAMEWORK
        get => Thread.CurrentThread.CurrentCulture;
        set => Thread.CurrentThread.CurrentCulture = value;
#else
        get => CultureInfo.CurrentCulture;
        set => CultureInfo.CurrentCulture = value;
#endif
    }

    private static CultureInfo CurrentUICulture
    {
#if NETFRAMEWORK
        get => Thread.CurrentThread.CurrentUICulture;
        set => Thread.CurrentThread.CurrentUICulture = value;
#else
        get => CultureInfo.CurrentUICulture;
        set => CultureInfo.CurrentUICulture = value;
#endif
    }
}