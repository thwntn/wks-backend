namespace Wks.Ports;

///
/// Summary:
///     Port for invalidating cached permission codes used by CheckPermission.
///
public interface IPermissionCachePort
{
    ///
    /// Summary:
    ///     Clears AUTH_PERMISSION caches for the account and its linked parent.
    ///
    /// Parameters:
    ///     accountId:
    ///         The account whose permission cache must be dropped.
    ///
    Task RemoveAsync(Guid accountId);
}
