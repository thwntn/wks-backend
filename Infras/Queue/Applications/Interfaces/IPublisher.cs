namespace Wks.Interfaces;

///
/// Summary:
///     Queues messages for background publication without dropping accepted work.
///     Completion means the bounded queue accepted the message, not broker delivery.
///
public interface IQueuePublisher
{
    Task PublishAsync<TMessage>(TMessage message)
        where TMessage : class;
}
