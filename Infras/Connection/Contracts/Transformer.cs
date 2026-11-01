namespace Wks.DataTransformers;

///
/// Summary:
///     Data transformer for SignalR hub connection messages.
///     Handles data validation and structure for connection-related operations.
///
public class ConnectionHubDataTransformer
{
    ///
    /// Summary:
    ///     Data structure for establishing a SignalR connection with a unique identifier.
    ///     Requires a valid connection ID for group-based message routing.
    ///
    public class Connect
    {
        [JsonRequired]
        public Guid connectId;
    }
}
