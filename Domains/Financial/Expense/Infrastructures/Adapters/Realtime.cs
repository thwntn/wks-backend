namespace Wks.Adapters;

public class RealtimeExpenseAdapter(ISocket _socket) : IRealtimeExpensePort
{
    public async Task ExecuteAsync(Guid profileId) =>
        await _socket.InvokeAsync(profileId, nameof(HubMethodName.EXPENSE));
}
