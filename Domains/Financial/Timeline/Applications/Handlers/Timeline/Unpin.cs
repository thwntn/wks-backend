namespace Wks.Handlers;

public class UnpinTimelineHandler(
    IUnpinTimeline _unpinTimeline,
    IRealtimeTimelinePort _realtimePort,
    IUnitOfWork _unitOfWork
) : IUnpinTimelineHandler
{
    public async Task<Timeline> ExecuteAsync(Guid identifier, Guid id)
    {
        Timeline timeline = await _unpinTimeline.ExecuteAsync(identifier, id);
        await _unitOfWork.SyncAsync();
        await _realtimePort.ExecuteAsync(identifier);
        return timeline;
    }
}
