namespace Wks.UseCases;

public class ListDomain(IDomainRepository _domainRepository) : IListDomain
{
    public async Task<List<Domain>> ExecuteAsync(Guid profileId) =>
        await _domainRepository
            .Where(domain =>
                domain.ProfileId == profileId
                && domain.DeletedAt.HasValue == false
            )
            .OrderByDescending(domain => domain.CreatedAt)
            .ToListAsync();
}
