namespace Wks.Adapters;

public class RealtimeTimelineAdapter(ISocket _socket) : IRealtimeTimelinePort
{
    public async Task ExecuteAsync(Guid profileId) =>
        await _socket.InvokeAsync(profileId, nameof(HubMethodName.FINANCIAL_TIMELINE));
}
