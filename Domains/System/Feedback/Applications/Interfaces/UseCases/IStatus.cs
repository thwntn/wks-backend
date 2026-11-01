namespace Wks.Interfaces;

public interface IListStatusFeedback
{
    Task<List<FeedbackObject.Status>> ExecuteAsync();
}
