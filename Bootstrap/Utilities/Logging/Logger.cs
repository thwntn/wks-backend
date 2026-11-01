using System.Threading.Channels;

namespace Wks.Mains;

///
/// Summary:
///     Provides an asynchronous console logging pipeline for application events.
///     Buffers structured log entries in a bounded channel and writes them in batches.
///
public class Logger
{
    ///
    /// Summary:
    ///     Enables structured logging only in Production for all callers.
    ///
    private static readonly bool Logging = Runtime.Mode.Env().Equals(
        nameof(SystemModeEnum.Production),
        StringComparison.OrdinalIgnoreCase
    );

    ///
    /// Summary:
    ///     Defines the shared header and job parameter key used to transport the trace identifier.
    ///
    public const string TRACE_HEADER = nameof(HeaderNames.Referer);
    private static readonly AsyncLocal<string> CurrentTrace = new();

    ///
    /// Summary:
    ///     Reads the trace identifier stored in the current asynchronous Logger scope.
    ///
    /// Returns:
    ///     The current trace identifier, or null when no Logger scope has assigned a trace.
    ///
    public static string TraceId => CurrentTrace.Value;

    ///
    /// Summary:
    ///     Opens an asynchronous logging scope and remembers the previously active trace.
    ///
    /// Logic:
    ///     Creates a version 7 GUID as the initial trace for the new scope.
    ///     Uses the supplied identifier when it passes the existing Justifiable check.
    ///     Returns a disposable scope that restores the previous AsyncLocal value.
    ///
    /// Parameters:
    ///   traceId:
    ///     The caller trace to restore, or null to start a new trace.
    ///
    /// Returns:
    ///     A scope that must be disposed after the correlated work finishes.
    ///
    public static IDisposable BeginScope(string traceId)
    {
        string  previous = CurrentTrace.Value;
        CurrentTrace.Value = string.Concat(Guid.CreateVersion7());
        traceId.Justifiable(traceId => CurrentTrace.Value = traceId);
        return new TraceScope(previous);
    }

    ///
    /// Summary:
    ///     Retains the trace that must be restored when a logging scope ends.
    ///
    /// Parameters:
    ///   previous:
    ///     The AsyncLocal trace value that was active before the scope opened.
    ///
    private sealed class TraceScope(string previous) : IDisposable
    {
        ///
        /// Summary:
        ///     Restores the previous trace in the current asynchronous context.
        ///
        public void Dispose() => CurrentTrace.Value = previous;
    }

    private static readonly int MAX_BOUNDED = 10000;
    private static readonly int BATCH = 1000;
    private static readonly int DELAY = 500;

    public static readonly Channel<string> Pipeline = Channel.CreateBounded<string>(
        new BoundedChannelOptions(MAX_BOUNDED)
        {
            FullMode = BoundedChannelFullMode.DropOldest,
            SingleWriter = false,
            SingleReader = true,
        }
    );

    ///
    /// Summary:
    ///     Starts a background worker that consumes log lines from the channel.
    ///     Writes log output to the console in batches to reduce I/O overhead.
    ///
    public static void Worker() =>
        Task.Run(async () =>
        {
            while (await Pipeline.Reader.WaitToReadAsync())
            {
                await Task.Delay(DELAY);

                int count = 0;
                while (count++ < BATCH && Pipeline.Reader.TryRead(out string line))
                    line.Justifiable(Console.WriteLine);
            }
        });

    ///
    /// Summary:
    ///     Serializes a structured log entry and submits it to the existing asynchronous logger pipeline.
    ///
    /// Logic:
    ///     Captures the current trace, level, payload, and UTC timestamp in a Line object.
    ///     Attempts to enqueue the serialized entry without blocking the business operation.
    ///     The bounded pipeline drops its oldest entries when full.
    ///     Suppresses serialization and enqueue exceptions so logging does not interrupt application work.
    ///
    /// Parameters:
    ///   value:
    ///     The structured event payload to serialize.
    ///   level:
    ///     The log severity name; defaults to Information.
    ///
    public static void Write(object value, string level = nameof(LogLevel.Information))
    {
        if (Logging is false)
            return;

        try
        {
            Pipeline.Writer.TryWrite(new Line(value, level).Serialize());
        }
        catch { }
    }

    ///
    /// Summary:
    ///     Serializes each supplied value and writes it directly to the console in green.
    ///     Writes synchronously without creating structured Line entries or using the logger pipeline.
    ///
    /// Logic:
    ///     Saves the current console foreground color before switching it to Green.
    ///     Serializes each value with the shared Serialize helper and writes one console line per value.
    ///     Restores the saved color after all values have been written successfully.
    ///     Serialization or console write exceptions propagate to the caller and prevent color restoration.
    ///
    /// Parameters:
    ///   values:
    ///     The objects to write, supplied as individual arguments or a List of objects.
    ///     Values are serialized and written in the order they appear in the collection.
    ///
    public static void Log(params List<object> values)
    {
        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        values.ForEach(value => Console.WriteLine(value.Serialize()));
        Console.ForegroundColor = previousColor;
    }
}
