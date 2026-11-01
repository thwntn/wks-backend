namespace Wks.UseCases;

public class ListHashTag(IHashTagRepository _tagRepository) : IListHashTag
{
    public async Task<Paged<Hashtag>> ExecuteAsync(
        Guid profileId,
        int filter,
        QueryOptions queryOptions
    )
    {
        IQueryable<Hashtag> tags = _tagRepository
            .Where(tag => tag.ProfileId == profileId && tag.Filter == filter)
            .OrderByDescending(tag => tag.CreatedAt)
            .AsQueryable();

        return await Pagination.CreateAsync(tags, queryOptions);
    }
}
