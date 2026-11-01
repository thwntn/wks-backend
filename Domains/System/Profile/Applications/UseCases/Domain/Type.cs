namespace Wks.UseCases;

public class TypeDomain(ICache _cache) : ITypeDomain
{
    public async Task<List<DomainObject.Type>> ExecuteAsync() =>
        await _cache.CacheAsync(
            nameof(CacheNames.DOMAIN_TYPE),
            async () =>
            {
                string define = await File.ReadAllTextAsync(
                    $"{Directory.GetCurrentDirectory()}/Shared/Metadata/Master/System/DomainType.JSON"
                );
                return define.Deserialize<List<DomainObject.Type>>();
            }
        );
}
