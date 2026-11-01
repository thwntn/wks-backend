namespace Wks.Interfaces;

public interface IListProfile
{
    Task<Paged<Profile>> ExecuteAsync(QueryOptions queryOptions);
}
