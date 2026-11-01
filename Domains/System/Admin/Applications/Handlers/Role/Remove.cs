namespace Wks.Handlers;

///
/// Summary:
///     Coordinates administrator RemoveRoleHandler and saves through UnitOfWork.
///
public class RemoveRoleAdminHandler(
    IRemoveRoleAdminPort _removeRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IRemoveRoleAdminHandler
{
    ///
    /// Summary:
    ///     Stages domain changes, saves once, then completes configured side effects.
    /// Parameters:
    ///     removeParams: The identifiers and ownership data for removal.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public async Task<IEnumerable<Role>> ExecuteAsync(
        RoleObject.RemoveParams removeParams
    )
    {
        IEnumerable<Role> roles = await _removeRole.ExecuteAsync(removeParams);
        await _unitOfWork.SyncAsync();

        foreach (
            Guid accountId in roles.Select(role => role.AccountId).Distinct()
        )
            await _permissionCachePort.RemoveAsync(accountId);

        return roles;
    }
}
