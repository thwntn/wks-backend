namespace Wks.UseCases;

public class CreateDomain(IDomainRepository _domainRepository) : ICreateDomain
{
    public async Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Create create
    )
    {
        if (create.Type is DomainTypeEnum.SHOP)
            await CheckHostAsync(create.Host, create.Type);

        return _domainRepository.Create(
            new Domain(profileId, create.Host, create.Type)
        );
    }

    ///
    /// Summary:
    ///     Rejects a host already assigned to another active shop domain.
    /// Logic:
    ///     Redirect hosts may be shared. Deleted shop domains do not reserve a host.
    /// Parameters:
    ///     host: The requested host.
    ///     type: The requested domain type.
    /// Returns:
    ///     A task completing when the host is available.
    /// Exceptions:
    ///     HttpException when an active shop already uses the host.
    ///
    private async Task CheckHostAsync(string host, DomainTypeEnum type)
    {
        bool existedHost = await _domainRepository.Raw.AnyAsync(domain =>
            domain.Host == host
            && domain.Type == DomainTypeEnum.SHOP
            && domain.DeletedAt.HasValue == false
        );

        if (existedHost)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.DOMAIN_HOST_ALREADY_EXISTS
            );
    }
}
