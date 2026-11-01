namespace Wks.Handlers;

public class UpdateTimelineHandler(
    IUpdateTimeline _updateTimeline,
    IRealtimeTimelinePort _realtimePort,
    IUnitOfWork _unitOfWork,
    ICreateActionPublisher _createActionPublisher
) : IUpdateTimelineHandler
{
    public async Task<Timeline> ExecuteAsync(Guid identifier, TimelineDataTransformer.Update update)
    {
        Timeline timeline = await _updateTimeline.ExecuteAsync(identifier, update);
        await _unitOfWork.SyncAsync();
        await _realtimePort.ExecuteAsync(identifier);

        await _createActionPublisher.Execute(
            new([new(identifier, nameof(ActionNames.TIMELINE_UPDATE), timeline.Serialize())])
        );
        return timeline;
    }
}
