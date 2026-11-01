namespace Wks.Mains;

///
/// Summary:
///     Propagates the current trace while a RabbitMQ message passes through the send pipeline.
///     Uses the non-generic message context to support all message types.
///     Records pipeline completion or failure without changing message delivery behavior.
///
public class RabbitMQSendLoggingFilter : IFilter<SendContext>
{
    ///
    /// Summary:
    ///     Registers the operation name in the MassTransit diagnostic probe.
    ///
    /// Parameters:
    ///   context:
    ///     The diagnostic context that receives the filter scope.
    ///
    public void Probe(ProbeContext context) => context.CreateFilterScope(nameof(LogEventEnum.Send));
    ///
    /// Summary:
    ///     Executes the RabbitMQ send pipeline with trace correlation and lifecycle logging.
    ///
    /// Logic:
    ///     Uses the message trace header when available, otherwise the ambient logger trace.
    ///     Opens a trace scope and places its identifier in the outgoing message header.
    ///     Awaits the downstream pipeline and logs its completion with elapsed milliseconds.
    ///     Logs the exception type on failure, rethrows it, and restores the previous trace.
    ///
    /// Parameters:
    ///   context:
    ///     The message context containing headers and send metadata.
    ///   next:
    ///     The next MassTransit pipe to execute.
    ///
    /// Returns:
    ///     A task that completes when the downstream pipeline finishes; failures propagate to MassTransit.
    ///
    public async Task Send(SendContext context, IPipe<SendContext> next)
    {
        /// Summary:
        ///     Reuses the trace already attached to the message, falling back to the caller scope.
        ///     This keeps outgoing transport logs correlated with the operation that produced them.
        string traceId = context.Headers.Get<string>(Logger.TRACE_HEADER) ?? Logger.TraceId;

        /// Summary:
        ///     Opens a scope for this transport operation and stores its trace in the message header.
        ///     The receiving service can restore that trace even when running in another process.
        ///     Disposing this scope restores the previous trace when the filter exits.
        using IDisposable scope = Logger.BeginScope(traceId);
        context.Headers.Set(Logger.TRACE_HEADER, Logger.TraceId);

        /// Summary:
        ///     Measures the downstream pipeline invoked by this filter.
        ///     This duration excludes work performed before reaching the filter.
        Stopwatch watch = Stopwatch.StartNew();
        try
        {
            /// Summary:
            ///     Awaits downstream processing before recording a successful outcome.
            ///     Transport completion does not imply completion of a remote consumer;
            ///     the consume filter records its own processing outcome separately.
            await next.Send(context);

            /// Summary:
            ///     Records the operation, message type, and elapsed time under the service index.
            ///     Logger supplies the active trace and controls output by runtime mode.
            Logger.Write(new LoggingSchema.RabbitMQ { LogIndexName = Runtime.LogIndexName.Env(), Source = nameof(LogEventEnum.RabbitMQ), Operation = nameof(LogEventEnum.Send), Status = nameof(LogEventEnum.Completed),
                MessageType = context.SupportedMessageTypes.FirstOrDefault(), DurationMs = watch.Elapsed.TotalMilliseconds });
        }
        catch (Exception error)
        {
            /// Summary:
            ///     Records the failed pipeline invocation without swallowing its exception.
            ///     Rethrowing preserves MassTransit fault handling and configured retry behavior.
            ///     The caller or consumer pipeline remains responsible for the final delivery outcome.
            Logger.Write(new LoggingSchema.RabbitMQ { LogIndexName = Runtime.LogIndexName.Env(), Source = nameof(LogEventEnum.RabbitMQ), Operation = nameof(LogEventEnum.Send), Status = nameof(LogEventEnum.Failed),
                MessageType = context.SupportedMessageTypes.FirstOrDefault(), DurationMs = watch.Elapsed.TotalMilliseconds,
                Error = error.GetType().Name }, nameof(LogLevel.Error));
            throw;
        }
    }
}
