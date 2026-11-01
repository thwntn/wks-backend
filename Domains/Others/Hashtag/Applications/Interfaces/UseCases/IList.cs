namespace Wks.Interfaces;

public interface IListHashTag
{
    Task<Paged<Hashtag>> ExecuteAsync(
        Guid profileId,
        int filter,
        QueryOptions queryOptions
    );
}
