namespace Wks.UseCases;

///
/// Summary:
///     SignalR hub for managing real-time WebSocket connections.
///     Handles connection lifecycle, initialization, and ping-pong keepalive messages.
///
public class SignalrHub(ISocket _connect) : Hub
{
    ///
    /// Summary:
    ///     Handles client disconnection events.
    ///     Called automatically when a client disconnects from the SignalR hub.
    ///
    /// Parameters:
    ///     exception:
    ///         Exception that caused the disconnection, if any.
    ///
    /// Returns:
    ///     Completed task representing the disconnection handling.
    ///
    public override Task OnDisconnectedAsync(Exception exception) =>
        base.OnDisconnectedAsync(exception);

    ///
    /// Summary:
    ///     Initializes a client connection by adding it to a group based on the connect ID.
    ///     Enables targeted message delivery to specific client groups.
    ///
    /// Parameters:
    ///     data:
    ///         Connection data containing the unique connect identifier.
    ///
    [HubMethodName(nameof(HubMethodName.INITIALIZE))]
    public void Initialize(ConnectionHubDataTransformer.Connect data) =>
        _connect.Initialize(Context.ConnectionId, data.connectId);

    ///
    /// Summary:
    ///     Responds to ping messages from clients to maintain connection keepalive.
    ///     Returns a pong response to confirm the connection is active.
    ///
    /// Returns:
    ///     PONG string response.
    ///
    [HubMethodName(nameof(HubMethodName.PING))]
    public string Ping() => nameof(HubMethodName.PONG);
}
