namespace Wks.Handlers;

///
/// Summary:
///     Handler for removing an actor role and committing the transaction.
///
public class RemoveRoleActorHandler(
    IRemoveRoleActor _removeRoleActor,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IRemoveRoleActorHandler
{
    ///
    /// Summary:
    ///     Removes an actor role and persists the staged changes.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     roleId:
    ///         The role identifier to remove.
    ///
    /// Returns:
    ///     The removed roles.
    ///
    /// Logic:
    ///     Delegates the owned actor role change and saves the staged changes once.
    ///     Invalidates the actor and parent permission caches after a successful save.
    ///
    public async Task<IEnumerable<Role>> ExecuteAsync(
        Guid profileId,
        Guid roleId
    )
    {
        IEnumerable<Role> roles = await _removeRoleActor.ExecuteAsync(
            profileId,
            roleId
        );
        await _unitOfWork.SyncAsync();
        foreach (
            Guid accountId in roles.Select(role => role.AccountId).Distinct()
        )
            await _permissionCachePort.RemoveAsync(accountId);

        return roles;
    }
}
