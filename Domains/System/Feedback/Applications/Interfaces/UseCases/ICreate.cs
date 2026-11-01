namespace Wks.Interfaces;

public interface ICreateFeedback
{
    Feedback Execute(Guid profileId, FeedbackObject.Create createParams);
}
