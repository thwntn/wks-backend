namespace Wks.UseCases;

public class ListProfile(IProfileRepository _profileRepository) : IListProfile
{
    public async Task<Paged<Profile>> ExecuteAsync(QueryOptions queryOptions)
    {
        IQueryable<Profile> profiles = _profileRepository.Raw.OrderByDescending(
            profile => profile.CreatedAt
        );

        return await Pagination.CreateAsync(profiles, queryOptions);
    }
}
