namespace Wks.Queues;

public class SetupAccountAuthConsumer(
    ISetupAccountAuthHandler _setupAccountAuthHandler
) : IConsumer<AuthBroker.InitialAccountMessage>
{
    public async Task Consume(
        ConsumeContext<AuthBroker.InitialAccountMessage> messageContext
    ) =>
        await _setupAccountAuthHandler.ExecuteAsync(
            messageContext.Message.AccountId
        );
}
