namespace Wks.Handlers;

public class CreateFeedbackHandler(
    ICreateFeedback _createFeedback,
    IUnitOfWork _unitOfWork,
    ISocket _socket
) : ICreateFeedbackHandler
{
    public async Task<Feedback> ExecuteAsync(
        Guid profileId,
        FeedbackObject.Create createParams
    )
    {
        Feedback feedback = _createFeedback.Execute(profileId, createParams);
        await _unitOfWork.SyncAsync();

        await _socket.InvokeAsync(
            profileId,
            nameof(HubMethodName.FEEDBACK),
            default
        );
        return feedback;
    }
}
