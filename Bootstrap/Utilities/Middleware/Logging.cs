namespace Wks.Mains;

///
/// Summary:
///     Middleware that records HTTP request execution and sends structured logs to Grafana.
///     Logger controls output by runtime mode; request bodies are attached on failed mutating requests.
///
public class GrafanaLogging(RequestDelegate next)
{
    ///
    /// Summary:
    ///     The next middleware in the HTTP pipeline.
    ///
    /// Returns:
    ///     The request delegate invoked after this middleware starts timing the request.
    ///
    private readonly RequestDelegate _next = next;

    ///
    /// Summary:
    ///     Stream position used to rewind the request body before and after reading.
    ///
    /// Returns:
    ///     Zero, the start of the request body stream.
    ///
    private const int START_POSITION = 0;

    ///
    /// Summary:
    ///     Times the request, captures an unhandled exception, and writes a Grafana log.
    ///
    /// Parameters:
    ///     httpContext:
    ///         The current HTTP context containing request and response information.
    ///
    /// Returns:
    ///     A task representing the asynchronous middleware execution.
    ///
    public async Task Invoke(HttpContext httpContext)
    {
        /// Logic:
        ///     Decide whether the request body may be logged and enable buffering when it can.
        ///     Run the rest of the pipeline and keep any unhandled exception for the log.
        ///     Pass the Grafana document to Logger in finally for centralized output control.
        ///
        using IDisposable trace = Logger.BeginScope(default);
        httpContext.TraceIdentifier = Logger.TraceId;
        Exception exception = default;
        Stopwatch stopWatch = Stopwatch.StartNew();
        bool print = Condition(httpContext);

        try
        {
            await _next(httpContext);
        }
        catch (Exception error)
        {
            exception = error;
            throw;
        }
        finally
        {
            /// Summary:
            ///     Retrieves exceptions handled by the inner error middleware after it writes
            ///     the final response status. Keeps failure details without guessing a status code.
            IExceptionHandlerFeature failure = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (failure.Justifiable())
                exception = failure.Error;

            await Stack(httpContext, stopWatch, exception, print);
        }
    }

    ///
    /// Summary:
    ///     Decides whether the request body is eligible to be captured later.
    ///     Only POST, PUT, and PATCH bodies within the configured size limit are accepted.
    ///
    /// Parameters:
    ///     httpContext:
    ///         The current HTTP context containing the incoming request.
    ///
    /// Returns:
    ///     True when buffering is enabled and the body may be logged, otherwise false.
    ///
    private static bool Condition(HttpContext httpContext)
    {
        /// Logic:
        ///     Accept only POST, PUT, or PATCH.
        ///     Reject bodies larger than MaxRequestBodyLogSize.
        ///     Enable buffering so the body can be reread after the action consumes it.
        ///
        bool method =
            httpContext.Request.Method == HttpMethods.Post
            || httpContext.Request.Method == HttpMethods.Put
            || httpContext.Request.Method == HttpMethods.Patch;

        bool withinLimit =
            httpContext.Request.ContentLength.HasValue == false
            || httpContext.Request.ContentLength.Value
                <= long.Parse(Runtime.MaxRequestBodyLogSize.Env());

        if (method == false || withinLimit == false)
            return false;

        httpContext.Request.EnableBuffering();
        return true;
    }

    ///
    /// Summary:
    ///     Builds a structured log document and writes it to Grafana.
    ///     The request body is attached only when the request is eligible and the level is not Information.
    ///
    /// Parameters:
    ///     httpContext:
    ///         The HTTP context containing request and response data.
    ///
    ///     stopWatch:
    ///         The stopwatch used to calculate total execution time.
    ///
    ///     exception:
    ///         The unhandled exception captured from the pipeline, or null when none was thrown.
    ///
    ///     print:
    ///         True when the request body may be read and attached to the log.
    ///
    /// Returns:
    ///     A task representing the asynchronous write to Grafana.
    ///
    private static async Task Stack(
        HttpContext httpContext,
        Stopwatch stopWatch,
        Exception exception,
        bool print
    )
    {
        /// Logic:
        ///     Resolve the log level from the exception and response status.
        ///     Fill request metadata on the log document.
        ///     Attach the request body on Warning or Error when Condition allowed it.
        ///     Copy exception details when an unhandled exception exists.
        ///     Write the document to Grafana at the resolved level.
        ///
        string safe = Safe(httpContext, exception);

        StackInformation message = new(
            httpContext.Response.StatusCode,
            stopWatch.Elapsed.TotalMilliseconds,
            httpContext.Request.QueryString.Value,
            httpContext.Request.Path.Value,
            httpContext.Request.Protocol,
            Runtime.LogIndexName.Env(),
            httpContext.Request.Method
        );

        if (safe is not nameof(LogLevel.Information) && print)
            message.Request = await Stream(httpContext);

        exception.Justifiable(exception =>
        {
            message.StackTrace = exception.StackTrace;
            message.ExceptionMessage = exception.Message;
        });

        Logger.Write(message, safe);
    }

    ///
    /// Summary:
    ///     Reads the request body as JSON after rewinding the buffered stream.
    ///
    /// Parameters:
    ///     httpContext:
    ///         The current HTTP context containing the request stream.
    ///
    /// Returns:
    ///     The deserialized request body object.
    ///
    private static async Task<object> Stream(HttpContext httpContext)
    {
        /// Logic:
        ///     Rewind the body stream to the start.
        ///     Read the full body without closing the stream.
        ///     Rewind again so remaining pipeline readers can consume the body.
        ///     Deserialize the JSON payload into an object.
        ///
        httpContext.Request.Body.Position = START_POSITION;

        using StreamReader reader = new(
            httpContext.Request.Body,
            leaveOpen: true
        );
        string request = await reader.ReadToEndAsync();

        httpContext.Request.Body.Position = START_POSITION;
        return request.Deserialize<object>(@default: request);
    }

    ///
    /// Summary:
    ///     Resolves the Grafana log level from an unhandled exception or the response status.
    ///
    /// Parameters:
    ///     httpContext:
    ///         The HTTP context whose response status is used when no exception exists.
    ///
    ///     exception:
    ///         The unhandled exception captured from the pipeline, or null when none was thrown.
    ///
    /// Returns:
    ///     Information when status is below 400, Warning for 400-499, Error for 500+ or any unhandled exception.
    ///
    private static string Safe(HttpContext httpContext, Exception exception)
    {
        /// Logic:
        ///     Treat any unhandled exception as Error.
        ///     Map status below 400 to Information, 400-499 to Warning, and 500+ to Error.
        ///
        if (exception.Justifiable())
            return nameof(LogLevel.Error);

        return httpContext.Response.StatusCode switch
        {
            < (int)HttpStatus.BadRequest => nameof(LogLevel.Information),
            < (int)HttpStatus.InternalServerError => nameof(LogLevel.Warning),
            _ => nameof(LogLevel.Error),
        };
    }
}
