namespace Wks.Mains;

///
/// Summary:
///     Defines the message payload schemas used by Redis, RabbitMQ, background jobs, and HTTP clients.
///     Logger wraps each payload in Line, which supplies one top-level traceId, level, and UTC timestamp.
///     Serialization uses camelCase; nullable fields are omitted when an event does not provide them.
///     LogIndexName is kept inside message for the existing Promtail service_name mapping.
///
public class LoggingSchema
{
    ///
    /// Summary:
    ///     Common event metadata and failure details used by all infrastructure sources.
    ///     Derived schemas add source-specific fields without changing the shared JSON field names.
    ///     Status describes the recorded operation; it does not imply downstream business completion.
    ///
    public class Event
    {
        ///
        /// Summary:
        ///     Identifies the service using the value supplied by the logging caller, for example ecmc-backend.
        ///     Serialized as message.logIndexName and mapped by Promtail to service_name.
        ///
        public string LogIndexName { get; set; }
        ///
        /// Summary:
        ///     Identifies the emitting subsystem, such as Redis, RabbitMQ, QueuePublisher, or Hangfire.
        ///     Used to distinguish operations within one service without changing the top-level trace.
        ///
        public string Source { get; set; }

        ///
        /// Summary:
        ///     Names the operation being measured, such as Get, Publish, Send, or Consume.
        ///     Hangfire execution uses the recurring registration ID, falling back to the job type.
        ///
        public string Operation { get; set; }

        ///
        /// Summary:
        ///     Names the lifecycle outcome, such as Started, Received, Completed, Failed, Hit, or Miss.
        ///     Can be omitted for events such as the publisher shutdown pending-count warning.
        ///
        public string Status { get; set; }

        ///
        /// Summary:
        ///     Elapsed milliseconds measured at the event call site.
        ///     QueuePublisher measures the full endpoint.Publish call; transport filters measure their downstream pipe.
        ///     Null means this event did not record a duration, rather than a duration of zero.
        ///
        public double? DurationMs { get; set; }

        ///
        /// Summary:
        ///     Exception type recorded by the emitting component.
        ///     Hangfire records the base exception type; other sources may record the caught exception type.
        ///
        public string Error { get; set; }

        ///
        /// Summary:
        ///     Human-readable error detail when supplied by the emitting component.
        ///     Hangfire uses the base exception message so wrapper exceptions do not hide the cause.
        ///
        public string ExceptionMessage { get; set; }

        ///
        /// Summary:
        ///     Diagnostic exception text or stack supplied by the emitting component.
        ///     Some callers use Exception.ToString() to preserve wrapper and inner exception details.
        ///
        public string StackTrace { get; set; }
    }

    ///
    /// Summary:
    ///     Redis cache and publication events identify the cache key or channel.
    ///     Get events can report Hit, Miss, or Failed; other operations report their own outcome.
    ///     DurationMs measures the Redis operation at the logging call site.
    ///
    public class Redis : Event
    {
        ///
        /// Summary:
        ///     Cache key or Redis publication channel associated with the operation.
        ///     For example, a redirect cache key or the socket:pipeline channel.
        ///
        public string Key { get; set; }
    }

    ///
    /// Summary:
    ///     RabbitMQ pipeline and bounded publisher events carry message and retry metadata.
    ///     Pipeline filters and QueuePublisher measure different portions of delivery.
    ///     Publish/send completion does not confirm that a remote consumer finished processing.
    ///
    public class RabbitMQ : Event
    {
        ///
        /// Summary:
        ///     CLR message type or MassTransit-supported message type identifier.
        ///     The representation depends on whether QueuePublisher or a transport filter emits the event.
        ///
        public string MessageType { get; set; }

        ///
        /// Summary:
        ///     Identifier assigned by QueuePublisher and reused across publish attempts.
        ///     Not supplied by every transport filter; a stable ID does not by itself prevent duplicates.
        ///
        public Guid? MessageId { get; set; }

        ///
        /// Summary:
        ///     Delay in seconds before QueuePublisher retries the current failed publication.
        ///     Increases per failure up to the configured maximum; absent on successful events.
        ///
        public int? RetryDelaySeconds { get; set; }

        ///
        /// Summary:
        ///     Publisher work outstanding when the shutdown deadline is reached.
        ///     Includes producers waiting for admission, queued messages, and messages held by workers.
        ///
        public long? Pending { get; set; }
    }

    ///
    /// Summary:
    ///     Hangfire and Quartz execution events identify the scheduled job or attempt.
    ///     The same Hangfire job can emit multiple Started/Failed events as retries occur.
    ///     DurationMs measures an execution attempt and excludes prior scheduler waiting time.
    ///
    public class Job : Event
    {
        ///
        /// Summary:
        ///     Hangfire persisted job ID or Quartz execution FireInstanceId.
        ///     Use together with Operation and the top-level traceId to correlate execution and retries.
        ///
        public string JobId { get; set; }
    }

    ///
    /// Summary:
    ///     Outgoing HTTP operations report their target and received status code.
    ///     This schema describes IRequest calls; inbound Grafana request logs use StackInformation.
    ///     A transport exception can produce Failed without a received HTTP status code.
    ///
    public class Http : Event
    {
        ///
        /// Summary:
        ///     Hostname of an outgoing HTTP target extracted from its absolute URI.
        ///     An empty value means the target could not be parsed as an absolute URI.
        ///
        public string Host { get; set; }

        ///
        /// Summary:
        ///     HTTP response status received by an outgoing IRequest operation.
        ///     Absent before a response is received or when the operation fails with a transport exception.
        ///
        public int? StatusCode { get; set; }
    }
}

///
/// Summary:
///     Represents a log entry that is written to the logger pipeline.
///     Includes log level, message payload, and timestamp metadata.
///
public class Line(object message, string level)
{
    ///
    /// Summary:
    ///     Captures the ambient trace when this entry is created for correlation across infrastructure events.
    ///
    public string TraceId { get; set; } = Logger.TraceId;
    ///
    /// Summary:
    ///     Severity passed to Logger.Write, such as Information, Warning, or Error.
    ///     Independent of payload Status: a failed event can retain its own operation metadata.
    ///
    public string Level { get; set; } = level;
    ///
    /// Summary:
    ///     Structured source-specific payload serialized beneath the message JSON field.
    ///     TraceId stays on this envelope rather than being duplicated in each payload.
    ///
    public object Message { get; set; } = message;
    ///
    /// Summary:
    ///     UTC time captured when the log entry is created, before asynchronous console output.
    ///     May differ from the collector ingestion time because Logger buffers entries.
    ///
    public DateTime Timestamp { get; set; } = Moment.Utc.Now;
}

///
/// Summary:
///     Structured payload sent to Grafana for one HTTP request.
///     Holds status, duration, route metadata, optional request body, and exception details.
///
public class StackInformation(
    int responseCode,
    double execute,
    string query,
    string pathname,
    string protocol,
    string logIndexName,
    string method
)
{
    ///
    /// Summary:
    ///     Identifies HTTP request logs alongside infrastructure log sources.
    ///
    public string Source { get; set; } = nameof(LogEventEnum.Http);

    ///
    /// Summary:
    ///     Gets or sets the HTTP response status code.
    ///
    /// Returns:
    ///     The response status visible after the inner exception handler finishes processing.
    ///     Intentionally acknowledged webhook failures can still return HTTP 200.
    ///
    public int ResponseCode { get; set; } = responseCode;

    ///
    /// Summary:
    ///     Gets or sets the request duration in milliseconds.
    ///
    /// Returns:
    ///     Elapsed time from middleware start until the log is built.
    ///
    public double Execute { get; set; } = execute;

    ///
    /// Summary:
    ///     Gets or sets the raw query string of the request.
    ///
    /// Returns:
    ///     The request query string, including the leading question mark when present.
    ///
    public string Query { get; set; } = query;

    ///
    /// Summary:
    ///     Gets or sets the request path.
    ///
    /// Returns:
    ///     The path portion of the request URL.
    ///
    public string Pathname { get; set; } = pathname;

    ///
    /// Summary:
    ///     Gets or sets the HTTP protocol version.
    ///
    /// Returns:
    ///     The protocol string from the current request, such as HTTP/1.1.
    ///
    public string Protocol { get; set; } = protocol;

    ///
    /// Summary:
    ///     Gets or sets the Grafana log index name.
    ///
    /// Returns:
    ///     The index name loaded from the runtime environment.
    ///
    public string LogIndexName { get; set; } = logIndexName;

    ///
    /// Summary:
    ///     Gets or sets the HTTP method.
    ///
    /// Returns:
    ///     The request method, such as GET or POST.
    ///
    public string Method { get; set; } = method;

    ///
    /// Summary:
    ///     Gets or sets the deserialized request body.
    ///
    /// Returns:
    ///     The JSON request body captured for eligible failed POST, PUT, or PATCH requests.
    ///     An empty string means no body was attached; it does not prove the request had no body.
    ///
    public object Request { get; set; } = string.Empty;

    ///
    /// Summary:
    ///     Gets or sets the unhandled exception message.
    ///
    /// Returns:
    ///     The exception message, or an empty string when no exception was thrown.
    ///
    public string ExceptionMessage { get; set; } = string.Empty;

    ///
    /// Summary:
    ///     Gets or sets the unhandled exception stack trace.
    ///
    /// Returns:
    ///     The stack trace, or an empty string when no exception was thrown.
    ///
    public string StackTrace { get; set; } = string.Empty;
}
