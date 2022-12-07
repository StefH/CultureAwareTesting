using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CultureAwareTesting.xUnit;

internal class CulturedXunitTheoryTestCaseRunner : XunitTheoryTestCaseRunner
{
    private readonly string _culture;
    private CultureInfo? _originalCulture;
    private CultureInfo? _originalUICulture;

    public CulturedXunitTheoryTestCaseRunner(
        CulturedXunitTheoryTestCase culturedXunitTheoryTestCase,
        string displayName,
        string skipReason,
        object[] constructorArguments,
        IMessageSink diagnosticMessageSink,
        IMessageBus messageBus,
        ExceptionAggregator aggregator,
        CancellationTokenSource cancellationTokenSource)
        : base(culturedXunitTheoryTestCase, displayName, skipReason, constructorArguments, diagnosticMessageSink, messageBus, aggregator, cancellationTokenSource)
    {
        _culture = culturedXunitTheoryTestCase.Culture;
    }

    protected override Task AfterTestCaseStartingAsync()
    {
        try
        {
            _originalCulture = CurrentCulture;
            _originalUICulture = CurrentUICulture;

            var cultureInfo = new CultureInfo(_culture);
            CurrentCulture = cultureInfo;
            CurrentUICulture = cultureInfo;
        }
        catch (Exception ex)
        {
            Aggregator.Add(ex);
            return Task.FromResult(0);
        }

        return base.AfterTestCaseStartingAsync();
    }

    protected override Task BeforeTestCaseFinishedAsync()
    {
        if (_originalUICulture != null)
        {
            CurrentUICulture = _originalUICulture;
        }

        if (_originalCulture != null)
        {
            CurrentCulture = _originalCulture;
        }

        return base.BeforeTestCaseFinishedAsync();
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