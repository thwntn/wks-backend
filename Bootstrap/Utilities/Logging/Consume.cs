namespace Wks.Mains;

///
/// Summary:
///     Restores the publisher trace while a RabbitMQ message passes through the consumer pipeline.
///     Records receipt, processing completion, and processing failure through Logger.Write.
///
public class RabbitMQConsumeLoggingFilter : IFilter<ConsumeContext>
{
    ///
    /// Summary:
    ///     Registers the operation name in the MassTransit diagnostic probe.
    ///
    /// Parameters:
    ///   context:
    ///     The diagnostic context that receives the filter scope.
    ///
    public void Probe(ProbeContext context) => context.CreateFilterScope(nameof(LogEventEnum.Consume));
    ///
    /// Summary:
    ///     Executes the RabbitMQ consume pipeline with trace correlation and lifecycle logging.
    ///
    /// Logic:
    ///     Reads the trace header and opens an asynchronous scope for downstream consumer work.
    ///     Writes Received before processing, then Completed with elapsed milliseconds on success.
    ///     Writes Failed with the exception type and rethrows the original exception.
    ///     Disposes the scope to restore the trace that was active before consumption.
    ///
    /// Parameters:
    ///   context:
    ///     The message context containing headers and consume metadata.
    ///   next:
    ///     The next MassTransit pipe to execute.
    ///
    /// Returns:
    ///     A task that completes when the downstream pipeline finishes; failures propagate to MassTransit.
    ///
    public async Task Send(ConsumeContext context, IPipe<ConsumeContext> next)
    {
        /// Summary:
        ///     Restores the producer trace from the incoming message header for consumer work.
        ///     A missing trace header starts a new scope; disposal restores the previous trace.
        using IDisposable scope = Logger.BeginScope(context.Headers.Get<string>(Logger.TRACE_HEADER));

        /// Summary:
        ///     Records receipt before invoking the downstream consume pipeline.
        ///     Received does not mean that consumer processing has succeeded.
        Logger.Write(new LoggingSchema.RabbitMQ { LogIndexName = Runtime.LogIndexName.Env(), Source = nameof(LogEventEnum.RabbitMQ), Operation = nameof(LogEventEnum.Consume), Status = nameof(LogEventEnum.Received), MessageType = context.SupportedMessageTypes.FirstOrDefault() });

        /// Summary:
        ///     Measures the downstream pipeline invoked by this filter.
        ///     This duration excludes work performed before reaching the filter.
        Stopwatch watch = Stopwatch.StartNew();
        try
        {
            /// Summary:
            ///     Awaits downstream consumer processing before recording completion.
            ///     DurationMs describes this pipeline invocation, not prior queue waiting time.
            await next.Send(context);

            /// Summary:
            ///     Records the operation, message type, and elapsed time under the service index.
            ///     Logger supplies the active trace and controls output by runtime mode.
            Logger.Write(new LoggingSchema.RabbitMQ { LogIndexName = Runtime.LogIndexName.Env(), Source = nameof(LogEventEnum.RabbitMQ), Operation = nameof(LogEventEnum.Consume), Status = nameof(LogEventEnum.Completed),
                MessageType = context.SupportedMessageTypes.FirstOrDefault(), DurationMs = watch.Elapsed.TotalMilliseconds });
        }
        catch (Exception error)
        {
            /// Summary:
            ///     Records the failed pipeline invocation without swallowing its exception.
            ///     Rethrowing preserves MassTransit fault handling and configured retry behavior.
            ///     The caller or consumer pipeline remains responsible for the final delivery outcome.
            Logger.Write(new LoggingSchema.RabbitMQ { LogIndexName = Runtime.LogIndexName.Env(), Source = nameof(LogEventEnum.RabbitMQ), Operation = nameof(LogEventEnum.Consume), Status = nameof(LogEventEnum.Failed),
                MessageType = context.SupportedMessageTypes.FirstOrDefault(), DurationMs = watch.Elapsed.TotalMilliseconds,
                Error = error.GetType().Name }, nameof(LogLevel.Error));
            throw;
        }
    }
}
