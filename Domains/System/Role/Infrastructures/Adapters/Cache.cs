namespace Wks.Adapters;

///
/// Summary:
///     Adapter for invalidating account-scoped permission caches after role changes.
///
public class PermissionCacheAdapter(
    ICache _cache,
    IAccountRepository _accountRepository
) : IPermissionCachePort
{
    ///
    /// Summary:
    ///     Clears permission caches affected by a role change on an account.
    ///
    /// Parameters:
    ///     accountId:
    ///         The account whose role or permission assignments changed.
    ///
    /// Logic:
    ///     Removes the changed account cache using its account ID.
    ///     Resolves the parent link and removes the parent aggregated cache when present.
    ///
    public async Task RemoveAsync(Guid accountId)
    {
        await _cache.RemoveAsync(
            Format.ToCacheName(nameof(CacheNames.AUTH_PERMISSION), accountId)
        );

        Account account = await _accountRepository.Raw.FirstOrDefaultAsync(
            account => account.Id == accountId
        );
        if (
            account.Justifiable() is false
            || account.Identifier.Justifiable() is false
        )
            return;

        await _cache.RemoveAsync(
            Format.ToCacheName(
                nameof(CacheNames.AUTH_PERMISSION),
                account.Identifier.Value
            )
        );
    }
}
