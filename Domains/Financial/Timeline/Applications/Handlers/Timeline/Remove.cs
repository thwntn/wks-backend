namespace Wks.Handlers;

public class RemoveTimelineHandler(
    IRemoveTimeline _removeTimeline,
    IRealtimeTimelinePort _realtimePort,
    IUnitOfWork _unitOfWork,
    ICreateActionPublisher _createActionPublisher
) : IRemoveTimelineHandler
{
    public async Task<string> ExecuteAsync(Guid identifier, Guid id)
    {
        string message = _removeTimeline.Execute(identifier, id);
        await _unitOfWork.SyncAsync();
        await _realtimePort.ExecuteAsync(identifier);

        await _createActionPublisher.Execute(
            new([new(identifier, nameof(ActionNames.TIMELINE_DELETE), message)])
        );
        return message;
    }
}
