namespace Wks.UseCases;

///
/// Summary:
///     Manages SignalR hub connections and message broadcasting via Redis pub/sub.
///     Handles connection initialization and message delivery to client groups or all clients.
///
public class Socket(IHubContext<SignalrHub> hubContext, ICache _cache) : ISocket
{
    public static readonly StackExchange.Redis.RedisChannel CHANNEL_NAME =
        StackExchange.Redis.RedisChannel.Literal(Runtime.SocketChannel.Env());

    private readonly IHubContext<SignalrHub> _hubContext = hubContext;

    ///
    /// Summary:
    ///     Initializes a SignalR connection by adding the client to a group.
    ///     Enables targeted message delivery to clients within the same group.
    ///
    /// Parameters:
    ///     connectionId:
    ///         The SignalR connection identifier for the connecting client.
    ///     identifier:
    ///         The unique identifier used as the group name for message routing.
    ///
    /// Returns:
    ///     Completed task when the client is added to the group.
    ///
    public async Task Initialize(string connectionId, Guid identifier) =>
        await _hubContext.Groups.AddToGroupAsync(
            connectionId,
            string.Concat(identifier)
        );

    // /// Summary:
    // ///     Send a message to all clients in a specific group identified by the provided identifier using the specified hub method name and data.
    // ///     Send a message to identifier connected clients using the specified hub method name and data.
    // public async Task InvokeAsync(Guid identifier, string hubMethodName, object data = default) =>
    //     await _hubContext.Clients.Group(string.Concat(identifier)).SendAsync(hubMethodName, data);

    // /// Summary:
    // ///     Send a message to all clients using the specified hub method name and data.
    // ///    Send a message to all connected clients using the specified hub method name and data.
    // public async Task InvokeAsync(string hubMethodName, object data = default) =>
    //     await _hubContext.Clients.All.SendAsync(hubMethodName, data);

    ///
    /// Summary:
    ///     Sends a message to all clients in a specific group via Redis pub/sub.
    ///     Publishes the message to Redis channel for distributed delivery across server instances.
    ///
    /// Parameters:
    ///     identifier:
    ///         The unique identifier of the target client group.
    ///     hubMethodName:
    ///         The name of the hub method to invoke on target clients.
    ///     data:
    ///         Optional data payload to send with the message.
    ///
    /// Returns:
    ///     Completed task when the message is published to Redis.
    ///
    public async Task InvokeAsync(
        Guid identifier,
        string hubMethodName,
        object data = default
    ) =>
        await _cache.PublisherAsync(
            CHANNEL_NAME,
            new SocketObject.PipelineMessage(identifier, hubMethodName, data)
        );

    ///
    /// Summary:
    ///     Broadcasts a message to all connected clients via Redis pub/sub.
    ///     Uses empty GUID to indicate broadcast to all clients across all server instances.
    ///
    /// Parameters:
    ///     hubMethodName:
    ///         The name of the hub method to invoke on all clients.
    ///     data:
    ///         Optional data payload to send with the message.
    ///
    /// Returns:
    ///     Completed task when the message is published to Redis.
    ///
    public async Task InvokeAsync(
        string hubMethodName,
        object data = default
    ) =>
        await _cache.PublisherAsync(
            CHANNEL_NAME,
            new SocketObject.PipelineMessage(Guid.Empty, hubMethodName, data)
        );
}
