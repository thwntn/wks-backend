namespace Wks.Handlers;

public class UpdateTagTimelineHandler(
    IUpdateTagTimeline _updateTag,
    IRealtimeTimelinePort _realtimePort,
    IUnitOfWork _unitOfWork
) : IUpdateTagTimelineHandler
{
    public async Task<IEnumerable<TimelineTag>> ExecuteAsync(
        Guid identifier,
        Guid id,
        TimelineDataTransformer.UpdateTag updateTag
    )
    {
        IEnumerable<TimelineTag> tags = _updateTag.Execute(identifier, id, updateTag);
        await _unitOfWork.SyncAsync();
        await _realtimePort.ExecuteAsync(identifier);
        return tags;
    }
}
