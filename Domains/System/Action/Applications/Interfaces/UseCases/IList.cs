namespace Wks.Interfaces;

public interface IListAction
{
    Task<Paged<Databases.Action>> ExecuteAsync(
        Guid profileId,
        QueryOptions queryOptions
    );
}
