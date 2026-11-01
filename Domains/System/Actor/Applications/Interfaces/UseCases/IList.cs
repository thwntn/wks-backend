namespace Wks.Interfaces;

public interface IListActor
{
    Task<Paged<Account>> ExecuteAsync(Guid profileId, QueryOptions queryOptions);
}
