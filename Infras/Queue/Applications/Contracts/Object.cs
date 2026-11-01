namespace Wks.Objects;

public class QueueObject
{
    ///
    /// Summary:
    ///     Holds message data and correlation without retaining the caller's scope.
    ///     The same message identifier is used for every publication attempt.
    ///
    public class Message(object data, Type messageType, string traceId)
    {
        public object Data { get; } = data;
        public Type MessageType { get; } = messageType;
        public string TraceId { get; } = traceId;
        public Guid MessageId { get; } = Guid.CreateVersion7();
    }
}
