namespace Wks.Queues;

public class SetupAccountAuthPublisher(IQueuePublisher _publisher)
    : ISetupAccountAuthPublisher
{
    public Task Execute(Guid accountId) =>
        _publisher.PublishAsync(
            new AuthBroker.InitialAccountMessage(accountId)
        );
}
