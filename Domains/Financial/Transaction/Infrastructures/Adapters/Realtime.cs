namespace Wks.Adapters;

public class RealtimeTransactionAdapter(ISocket _socket) : IRealtimeTransactionPort
{
    public async Task ExecuteAsync(Guid profileId) =>
        await _socket.InvokeAsync(profileId, nameof(HubMethodName.EXPENSE_TRANSACTION));
}
