namespace Wks.UseCases;

public class GetDomain(IDomainRepository _domainRepository) : IGetDomain
{
    public async Task<Domain> ExecuteAsync(Guid profileId, Guid domainId) =>
        await _domainRepository.FirstOrDefaultAsync(domain =>
            domain.Id == domainId
            && domain.ProfileId == profileId
            && domain.DeletedAt.HasValue == false
        )
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_DOMAIN
        );
}
