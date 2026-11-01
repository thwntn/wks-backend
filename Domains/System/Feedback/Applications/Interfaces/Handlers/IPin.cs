namespace Wks.Interfaces;

public interface IPinFeedbackHandler
{
    Task<Feedback> ExecuteAsync(Guid profileId, Guid id);
}
