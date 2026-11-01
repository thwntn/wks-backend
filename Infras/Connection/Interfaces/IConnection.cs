namespace Wks.Interfaces;

///
/// Summary:
///     Interface for managing SignalR socket connections and message broadcasting.
///     Provides methods for connection initialization and message delivery to clients.
///
public interface ISocket
{
    ///
    /// Summary:
    ///     Initializes a SignalR connection by adding the client to a group.
    ///
    /// Parameters:
    ///     connectionId:
    ///         The SignalR connection identifier for the client.
    ///     identifier:
    ///         The unique identifier used to create a group for targeted messaging.
    ///
    Task Initialize(string connectionId, Guid identifier);

    ///
    /// Summary:
    ///     Sends a message to all clients in a specific group identified by connectId.
    ///
    /// Parameters:
    ///     connectId:
    ///         The unique identifier of the target group.
    ///     hubMethodName:
    ///         The name of the hub method to invoke on clients.
    ///     data:
    ///         Optional data payload to send with the message.
    ///
    Task InvokeAsync(
        Guid connectId,
        string hubMethodName,
        object data = default
    );

    ///
    /// Summary:
    ///     Broadcasts a message to all connected clients.
    ///
    /// Parameters:
    ///     hubMethodName:
    ///         The name of the hub method to invoke on all clients.
    ///     data:
    ///         Optional data payload to send with the message.
    ///
    Task InvokeAsync(string hubMethodName, object data = default);
}
