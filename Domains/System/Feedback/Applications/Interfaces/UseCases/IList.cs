namespace Wks.Interfaces;

public interface IListFeedback
{
    Task<Paged<Feedback>> ExecuteAsync(
        Guid profileId,
        QueryOptions queryOptions
    );
}
