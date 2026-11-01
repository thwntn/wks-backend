namespace Wks.UseCases;

public class ReplyFeedback(
    IFeedbackMessageRepository _feedbackMessageRepository
) : IReplyFeedback
{
    public FeedbackMessage Execute(
        Guid profileId,
        Guid feedbackId,
        FeedbackObject.Reply replyParams
    ) =>
        _feedbackMessageRepository.Create(
            new FeedbackMessage(
                profileId,
                feedbackId,
                replyParams.Message,
                replyParams.File
            )
        );
}
