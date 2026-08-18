using System;
using System.Globalization;

namespace Task.Tests;

public abstract class CurrencyCultureTests : IDisposable
{
    private readonly CultureInfo _original;

    protected CurrencyCultureTests()
    {
        _original = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("en-AU");
    }

    public void Dispose() => CultureInfo.CurrentCulture = _original;

    protected static string Lines(params string[] lines) =>
        string.Join(Environment.NewLine, lines);
}
