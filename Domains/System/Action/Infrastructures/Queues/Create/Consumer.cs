namespace Wks.Queues;

public class CreateActionConsumer(ICreateActionHandler _createActionHandler)
    : IConsumer<ActionBroker.CreateActionMessage>
{
    public async Task Consume(
        ConsumeContext<ActionBroker.CreateActionMessage> consumeContext
    )
    {
        await _createActionHandler.ExecuteAsync(consumeContext.Message.Actions);
    }
}
