namespace Wks.UseCases;

///
/// Summary:
///     Use case for clearing every declared cache namespace through the cache abstraction.
///
public class ClearCacheAdmin(ICache _cache) : IClearCacheAdmin
{
    ///
    /// Summary:
    ///     Removes root and descendant keys for every CacheNames value.
    /// Exceptions:
    ///     Redis failures propagate to the API.
    ///
    public async Task ExecuteAsync()
    {
        foreach (string name in Enum.GetNames<CacheNames>())
            await _cache.RemoveNamespaceAsync(name);
    }
}
