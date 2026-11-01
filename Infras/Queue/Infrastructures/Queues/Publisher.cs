using System.Threading.Channels;

namespace Wks.Queues;

///
/// Summary:
///     Publishes queued messages through a bounded background pipeline.
///     Producers wait for capacity; failed messages stay with their worker until sent.
///     An in-memory queue cannot survive a process crash or forced termination.
///
public class QueuePublisher(IServiceScopeFactory _scopeFactory)
    : BackgroundService, IQueuePublisher
{
    private static readonly int MAX_BOUNDED = Configure(Runtime.QueuePublisherCapacity);
    private static readonly int CONCURRENCY = Configure(Runtime.QueuePublisherConcurrency);
    private static readonly int PUBLISH_TIMEOUT_SECONDS = Configure(Runtime.QueuePublisherTimeoutSeconds);
    private static readonly int MAX_RETRY_DELAY_SECONDS = Configure(Runtime.QueuePublisherMaxRetryDelaySeconds);

    ///
    /// Summary:
    ///     Requires positive publisher settings so invalid configuration fails at startup.
    ///
    private static int Configure(Runtime name)
    {
        if (int.TryParse(name.Env(), out int value) && value > 0)
            return value;

        throw new InvalidOperationException($"{name} must be a positive integer.");
    }

    private long _pending;

    private readonly Channel<QueueObject.Message> _pipeline =
        Channel.CreateBounded<QueueObject.Message>(
            new BoundedChannelOptions(MAX_BOUNDED)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleWriter = false,
                SingleReader = false,
                AllowSynchronousContinuations = false,
            }
        );

    ///
    /// Summary:
    ///     Awaits capacity before returning to the caller.
    ///     Closed queues reject new work explicitly instead of silently losing it.
    ///
    public async Task PublishAsync<TMessage>(TMessage message)
        where TMessage : class
    {
        ArgumentNullException.ThrowIfNull(message);
        /// Summary:
        ///     Captures data, message type, and caller trace before leaving the request scope.
        ///     Pending includes messages waiting for admission, queued messages, and active workers.
        QueueObject.Message queued = new(message, typeof(TMessage), Logger.TraceId);
        Interlocked.Increment(ref _pending);
        try
        {
            /// Summary:
            ///     Waits when the bounded channel is full instead of dropping the message.
            ///     Returning confirms local queue admission, not RabbitMQ delivery.
            await _pipeline.Writer.WriteAsync(queued);
        }
        catch
        {
            /// Summary:
            ///     Removes the pending reservation when admission fails, such as after shutdown.
            ///     Propagates the failure so callers do not treat a rejected message as accepted.
            Interlocked.Decrement(ref _pending);
            throw;
        }
    }

    ///
    /// Summary:
    ///     Starts a fixed number of workers; no task is created per incoming request.
    ///
    protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
        Task.WhenAll(
            Enumerable.Range(0, CONCURRENCY).Select(_ => Worker(stoppingToken))
        );

    private async Task Worker(CancellationToken stoppingToken)
    {
        await foreach (QueueObject.Message message in _pipeline.Reader.ReadAllAsync(stoppingToken))
        {
            /// Summary:
            ///     Keeps this worker on the current message until publishing succeeds.
            ///     Only then decrements Pending and advances to the next queued message.
            await Publish(message, stoppingToken);
            Interlocked.Decrement(ref _pending);
        }
    }

    ///
    /// Summary:
    ///     Creates a fresh scope per attempt and retries the same message on failure.
    ///     A failed or timed-out publish never advances to the next queued message.
    ///     Retried deliveries can be duplicated and consumers must be idempotent.
    ///
    private async Task Publish(QueueObject.Message message, CancellationToken stoppingToken)
    {
        /// Summary:
        ///     Restores the producer trace for all attempts performed by this worker.
        ///     Retry delay belongs to this message and starts from zero for the next message.
        using IDisposable trace = Logger.BeginScope(message.TraceId);
        int retryDelay = 0;
        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            long started = 0;
            try
            {
                /// Summary:
                ///     Resolves a fresh scoped endpoint for each attempt so request-scoped
                ///     dependencies are not retained after the originating request finishes.
                await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();
                IPublishEndpoint endpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
                using CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
                /// Summary:
                ///     Bounds each publish attempt while also honoring worker cancellation.
                ///     A timeout triggers retry rather than discarding the current message.
                timeout.CancelAfter(TimeSpan.FromSeconds(PUBLISH_TIMEOUT_SECONDS));

                started = Stopwatch.GetTimestamp();
                /// Summary:
                ///     Measures the full endpoint.Publish call, not just the logging filter.
                ///     Reuses the message ID across attempts; duplicate delivery is still possible.
                await endpoint.Publish(
                    message.Data,
                    message.MessageType,
                    context => context.MessageId = message.MessageId,
                    timeout.Token
                );
                double durationMs = Stopwatch.GetElapsedTime(started).TotalMilliseconds;
                Logger.Write(new LoggingSchema.RabbitMQ
                {
                    LogIndexName = Runtime.LogIndexName.Env(),
                    Source = nameof(QueuePublisher),
                    Operation = nameof(LogEventEnum.Publish),
                    Status = nameof(LogEventEnum.Completed),
                    MessageType = message.MessageType.FullName,
                    MessageId = message.MessageId,
                    DurationMs = durationMs,
                });
                return;
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception error)
            {
                /// Summary:
                ///     Increases retry delay by one second up to the configured maximum.
                ///     Retains the current message and logs the failed attempt before waiting.
                retryDelay = Math.Min(retryDelay + 1, MAX_RETRY_DELAY_SECONDS);
                Logger.Write(new LoggingSchema.RabbitMQ
                {
                    LogIndexName = Runtime.LogIndexName.Env(),
                    Source = nameof(QueuePublisher),
                    MessageType = message.MessageType.FullName,
                    MessageId = message.MessageId,
                    Error = error.GetType().Name,
                    DurationMs = started == 0 ? (double?)null : Stopwatch.GetElapsedTime(started).TotalMilliseconds,
                    RetryDelaySeconds = retryDelay,
                }, nameof(LogLevel.Error));
                await Task.Delay(TimeSpan.FromSeconds(retryDelay), stoppingToken);
            }
        }
    }

    ///
    /// Summary:
    ///     Closes admission and drains accepted messages while the bus is still running.
    ///     Continues draining past the host deadline rather than discarding pending work.
    ///     Shutdown can wait indefinitely while the broker is unavailable.
    ///
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        /// Summary:
        ///     Rejects new admissions while allowing queued messages to reach RabbitMQ.
        ///     The host deadline records Pending but does not discard accepted messages.
        _pipeline.Writer.TryComplete();
        using CancellationTokenRegistration deadline = cancellationToken.Register(() =>
            Logger.Write(new LoggingSchema.RabbitMQ
            {
                LogIndexName = Runtime.LogIndexName.Env(),
                Source = nameof(QueuePublisher),
                Pending = Interlocked.Read(ref _pending),
            }, nameof(LogLevel.Warning))
        );

        if (ExecuteTask is not null)
            /// Summary:
            ///     Waits for workers to drain before the hosted service finishes stopping.
            ///     A broker outage can keep shutdown waiting indefinitely; forced termination
            ///     still loses messages because this channel is stored only in memory.
            await ExecuteTask;

        await base.StopAsync(CancellationToken.None);
    }
}
