namespace Wks.UseCases;

public class RemoveDomain(IDomainRepository _domainRepository) : IRemoveDomain
{
    public async Task ExecuteAsync(Guid profileId, List<Guid> ids)
    {
        List<Domain> domains = await _domainRepository.WhereAsync(domain =>
            ids.Contains(domain.Id)
            && domain.ProfileId == profileId
            && domain.DeletedAt.HasValue == false
        );

        _domainRepository.Remove(domains);
    }
}
