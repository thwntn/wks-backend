namespace Wks.UseCases;

///
/// Summary:
///     Resolves the storefront host sent in the "From" header to the owning profile identifier.
///     Looks up the active shop domain and caches it by host.
///
public class ResolveDomain(IDomainRepository _domainRepository, ICache _cache)
    : IResolveDomain
{
    public async Task<Guid> ExecuteAsync(string from)
    {
        Domain domain = await _cache.CacheAsync(
            Format.ToCacheName(nameof(CacheNames.SHOP_DOMAIN), from),
            async () =>
                await _domainRepository.FirstOrDefaultAsync(domain =>
                    domain.Host == from
                    && domain.Type == DomainTypeEnum.SHOP
                    && domain.DeletedAt.HasValue == false
                )
                ?? throw new HttpException(
                    HttpStatus.NotFound,
                    MessageConstant.NOT_FOUND_DOMAIN
                )
        );

        return domain.ProfileId;
    }
}
