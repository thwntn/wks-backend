namespace Wks.Queues;

public class CreateActionPublisher(IQueuePublisher _publisher)
    : ICreateActionPublisher
{
    public async Task Execute(ActionBroker.CreateActionMessage message) =>
        await _publisher.PublishAsync(message);
}
