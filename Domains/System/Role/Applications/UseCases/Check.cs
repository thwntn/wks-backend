namespace Wks.UseCases;

///
/// Summary:
///     Use case for checking permission codes owned by an account or its linked actors.
///     Caches the distinct owned codes by the account identifier from the JWT.
///
public class CheckPermission(
    IPermissionRepository _permissionRepository,
    ICache _cache
) : ICheckPermission
{
    ///
    /// Summary:
    ///     Checks whether an account matches at least one required permission code.
    ///
    /// Parameters:
    ///     accountId:
    ///         The unchanged root or actor account ID carried by the token.
    ///
    ///     permissions:
    ///         The accepted permission codes required by the authorization policy.
    ///
    /// Returns:
    ///     True when an owned permission matches any required code; false otherwise.
    ///
    /// Logic:
    ///     Loads the cached permission codes for the account or queries them on a miss.
    ///     Accepts roles whose account ID or parent Identifier matches the subject.
    ///     Compares the resulting codes against the required policy codes.
    ///
    public async Task<bool> ExecuteAsync(
        Guid accountId,
        List<string> permissions
    )
    {
        List<string> ownedPermissions = await _cache.CacheAsync(
            Format.ToCacheName(nameof(CacheNames.AUTH_PERMISSION), accountId),
            () => GetPermissions(accountId)
        );
        return ownedPermissions.Any(permissions.Contains);
    }

    ///
    /// Summary:
    ///     Loads distinct permission codes for the account and its linked actors.
    ///
    /// Parameters:
    ///     accountId:
    ///         The root or actor account ID used to scope role ownership.
    ///
    /// Returns:
    ///     The distinct codes from non-deleted roles matching the account scope.
    ///
    /// Logic:
    ///     Groups the account ID and parent Identifier alternatives before filtering roles.
    ///     Projects and deduplicates permission codes without loading role entities.
    ///
    private async Task<List<string>> GetPermissions(Guid accountId) =>
        await _permissionRepository
            .Where(permission =>
                (
                    permission.Role.Account.Id == accountId
                    || permission.Role.Account.Identifier == accountId
                )
                && permission.Role.DeletedAt.HasValue == false
            )
            .Select(permission => permission.Code)
            .Distinct()
            .ToListAsync();
}
