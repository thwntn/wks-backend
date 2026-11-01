namespace Wks.Subscribers;

///
/// Summary:
///     Background service for subscribing to Redis pub/sub messages and broadcasting to SignalR clients.
///     Listens to Redis socket channel and routes messages to appropriate SignalR client groups.
///
public class RedisSocketSubscriber(
    ICache _cache,
    IHubContext<SignalrHub> _hubContext
) : BackgroundService
{
    ///
    /// Summary:
    ///     Executes the background service to listen for Redis pub/sub messages.
    ///     Subscribes to the Redis socket channel and routes messages to SignalR clients.
    ///
    /// Parameters:
    ///     stoppingToken:
    ///         Cancellation token to stop the background service gracefully.
    ///
    /// Returns:
    ///     Task that runs indefinitely until the service is stopped.
    ///
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        /// Logic:
        ///     Subscribe to Redis channel and route messages based on identifier.
        ///
        await _cache.SignSubscriber<SocketObject.PipelineMessage>(
            Socket.CHANNEL_NAME,
            async (_, pipelineMessage) =>
            {
                if (pipelineMessage.Identifier == Guid.Empty)
                    await Every(pipelineMessage);
                else
                    await Only(pipelineMessage);
            }
        );

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    ///
    /// Summary:
    ///     Sends a message to all clients in a specific SignalR group.
    ///     Used when the message has a specific group identifier.
    ///
    /// Parameters:
    ///     pipelineMessage:
    ///         The message containing group identifier, method name, and data.
    ///
    /// Returns:
    ///     Completed task when the message is sent to the group.
    ///
    private async Task Every(SocketObject.PipelineMessage pipelineMessage) =>
        await pipelineMessage.HubMethodName.JustifiableAsync(
            async hubMethodName =>
                await _hubContext
                    .Clients.Group(string.Concat(pipelineMessage.Identifier))
                    .SendAsync(
                        pipelineMessage.HubMethodName,
                        pipelineMessage.Data
                    )
        );

    ///
    /// Summary:
    ///     Broadcasts a message to all connected SignalR clients.
    ///     Used when the message has an empty identifier indicating broadcast to all.
    ///
    /// Parameters:
    ///     pipelineMessage:
    ///         The message containing method name and data to broadcast.
    ///
    /// Returns:
    ///     Completed task when the message is broadcast to all clients.
    ///
    private async Task Only(SocketObject.PipelineMessage pipelineMessage) =>
        await pipelineMessage.HubMethodName.JustifiableAsync(
            async hubMethodName =>
                await _hubContext.Clients.All.SendAsync(
                    pipelineMessage.HubMethodName,
                    pipelineMessage.Data
                )
        );
}
