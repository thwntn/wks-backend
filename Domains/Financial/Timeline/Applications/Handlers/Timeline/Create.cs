namespace Wks.Handlers;

public class CreateTimelineHandler(
    ICreateTimeline _createTimeline,
    IRealtimeTimelinePort _realtimePort,
    IUnitOfWork _unitOfWork,
    ICreateActionPublisher _createActionPublisher
) : ICreateTimelineHandler
{
    public async Task<Timeline> ExecuteAsync(Guid profileId, TimelineDataTransformer.Create create)
    {
        Timeline result = _createTimeline.Execute(profileId, create);
        await _unitOfWork.SyncAsync();
        await _realtimePort.ExecuteAsync(profileId);

        await _createActionPublisher.Execute(
            new([new(profileId, nameof(ActionNames.TIMELINE_CREATE), result.Serialize())])
        );
        return result;
    }
}
