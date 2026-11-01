namespace Wks.Handlers;

public class ReplyFeedbackHandler(
    IReplyFeedback _replyFeedback,
    IUnitOfWork _unitOfWork,
    ISocket _socket
) : IReplyFeedbackHandler
{
    public async Task<FeedbackMessage> ExecuteAsync(
        Guid profileId,
        Guid feedbackId,
        FeedbackObject.Reply replyParams
    )
    {
        FeedbackMessage feedbackMessage = _replyFeedback.Execute(
            profileId,
            feedbackId,
            replyParams
        );

        await _unitOfWork.SyncAsync();
        await _socket.InvokeAsync(
            profileId,
            nameof(HubMethodName.FEEDBACK),
            default
        );
        return feedbackMessage;
    }
}
