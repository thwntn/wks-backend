namespace Wks.UseCases;

public class UpdateDomain(IDomainRepository _domainRepository) : IUpdateDomain
{
    public async Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Update update
    )
    {
        Domain current =
            await _domainRepository.FirstOrDefaultAsync(domain =>
                domain.Id == update.Id
                && domain.ProfileId == profileId
                && domain.DeletedAt.HasValue == false
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_DOMAIN
            );

        if (update.Type is DomainTypeEnum.SHOP)
            await CheckHostAsync(update.Host, update.Type, current.Id);

        Domain domain = current.Merge(update);
        _domainRepository.Update(domain);
        return domain;
    }

    ///
    /// Summary:
    ///     Rejects a host already assigned to another active shop domain.
    /// Logic:
    ///     Redirect hosts may be shared. Deleted shop domains do not reserve a host.
    /// Parameters:
    ///     host: The requested host.
    ///     type: The requested domain type.
    ///     domainId: The current domain excluded from the duplicate check.
    /// Returns:
    ///     A task completing when the host is available.
    /// Exceptions:
    ///     HttpException when an active shop already uses the host.
    ///
    private async Task CheckHostAsync(
        string host,
        DomainTypeEnum type,
        Guid domainId
    )
    {
        bool existedHost = await _domainRepository.Raw.AnyAsync(domain =>
            domain.Host == host
            && domain.Type == DomainTypeEnum.SHOP
            && domain.DeletedAt.HasValue == false
            && domain.Id != domainId
        );

        if (existedHost)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.DOMAIN_HOST_ALREADY_EXISTS
            );
    }
}
