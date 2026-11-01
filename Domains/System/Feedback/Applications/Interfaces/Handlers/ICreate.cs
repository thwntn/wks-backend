namespace Wks.Interfaces;

public interface ICreateFeedbackHandler
{
    Task<Feedback> ExecuteAsync(
        Guid profileId,
        FeedbackObject.Create createParams
    );
}
