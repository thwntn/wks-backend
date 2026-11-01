namespace Wks.UseCases;

public class ListAction(IActionRepository _actionRepository) : IListAction
{
    public async Task<Paged<Databases.Action>> ExecuteAsync(
        Guid profileId,
        QueryOptions queryOptions
    )
    {
        IQueryable<Databases.Action> actions = _actionRepository
            .Include(action => action.Profile)
            .Where(action => action.ProfileId == profileId)
            .OrderByDescending(action => action.CreatedAt);

        return await Pagination.CreateAsync(actions, queryOptions);
    }
}
