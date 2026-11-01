namespace Wks.Interfaces;

public interface IReplyFeedback
{
    FeedbackMessage Execute(
        Guid profileId,
        Guid feedbackId,
        FeedbackObject.Reply replyParams
    );
}
