namespace Wks.Interfaces;

public interface IUnpinFeedbackHandler
{
    Task<Feedback> ExecuteAsync(Guid profileId, Guid id);
}
