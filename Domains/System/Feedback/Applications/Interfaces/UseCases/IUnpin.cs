namespace Wks.Interfaces;

public interface IUnpinFeedback
{
    Task<Feedback> ExecuteAsync(Guid profileId, Guid id);
}
