namespace Wks.Interfaces;

public interface IPinFeedback
{
    Task<Feedback> ExecuteAsync(Guid profileId, Guid id);
}
