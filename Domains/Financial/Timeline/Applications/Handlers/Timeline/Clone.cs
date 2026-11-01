namespace Wks.Handlers;

public class CloneTimelineHandler(
    ICloneTimeline _cloneTimeline,
    IRealtimeTimelinePort _realtimePort,
    IUnitOfWork _unitOfWork,
    ICreateActionPublisher _createActionPublisher
) : ICloneTimelineHandler
{
    public async Task<List<Expense>> ExecuteAsync(Guid profileId, Guid fromId, Guid toId)
    {
        List<Expense> expenses = await _cloneTimeline.ExecuteAsync(profileId, fromId, toId);
        await _unitOfWork.SyncAsync();
        await _realtimePort.ExecuteAsync(profileId);

        /// Summary:
        ///    Record action for cloning timeline
        ///    This action can be used for logging, auditing, or triggering other processes related to the cloning of a timeline.
        await _createActionPublisher.Execute(
            new([new(profileId, nameof(ActionNames.TIMELINE_CREATE), expenses.Serialize())])
        );
        return expenses;
    }
}
