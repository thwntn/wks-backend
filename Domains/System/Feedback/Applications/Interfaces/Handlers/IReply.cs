namespace Wks.Interfaces;

public interface IReplyFeedbackHandler
{
    Task<FeedbackMessage> ExecuteAsync(
        Guid profileId,
        Guid feedbackId,
        FeedbackObject.Reply replyParams
    );
}
