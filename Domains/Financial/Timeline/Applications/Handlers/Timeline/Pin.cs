namespace Wks.Handlers;

public class PinTimelineHandler(
    IPinTimeline _pinTimeline,
    IRealtimeTimelinePort _realtimePort,
    IUnitOfWork _unitOfWork
) : IPinTimelineHandler
{
    public async Task<Timeline> ExecuteAsync(Guid identifier, Guid id)
    {
        Timeline timeline = await _pinTimeline.ExecuteAsync(identifier, id);
        await _unitOfWork.SyncAsync();
        await _realtimePort.ExecuteAsync(identifier);
        return timeline;
    }
}
