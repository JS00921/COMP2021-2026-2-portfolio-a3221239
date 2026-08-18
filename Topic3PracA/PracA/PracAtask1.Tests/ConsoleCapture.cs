using System;
using System.IO;

namespace PracAtask1.Tests;

public sealed class ConsoleCapture : IDisposable
{
    private readonly TextWriter originalOut;
    private readonly StringWriter capturedOut;

    public ConsoleCapture()
    {
        originalOut = Console.Out;
        capturedOut = new StringWriter();
        Console.SetOut(capturedOut);
    }

    public string Text => capturedOut.ToString();

    public string[] Lines =>
        Text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    public void Dispose()
    {
        Console.SetOut(originalOut);
        capturedOut.Dispose();
    }
}

[CollectionDefinition("Console")]
public class ConsoleCollection { }