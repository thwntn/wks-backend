namespace Wks.Objects;

///
/// Summary:
///     Represents a SignalR client hub connection with account and socket identification.
///     Stores client proxy for sending messages to specific connected clients.
///
public class ClientHub(
    string accountId,
    string socketId,
    IClientProxy clientProxy
)
{
    public IClientProxy clientProxy = clientProxy;
    public string socketId = socketId;
    public string accountId = accountId;
}

///
/// Summary:
///     Contains data transfer objects for socket-based communication.
///     Provides message structures for pipeline-based message delivery.
///
public class SocketObject
{
    ///
    /// Summary:
    ///     Represents a message sent through the Redis pipeline for SignalR hub communication.
    ///     Contains identifier for targeting specific groups, method name, and message data.
    ///
    public class PipelineMessage(
        Guid identifier,
        string hubMethodName,
        object data = default
    )
    {
        public Guid Identifier { get; set; } = identifier;
        public string HubMethodName { get; set; } = hubMethodName;
        public object Data { get; set; } = data;
    }
}
