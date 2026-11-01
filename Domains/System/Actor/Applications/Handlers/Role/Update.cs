namespace Wks.Handlers;

///
/// Summary:
///     Handler for updating an actor role and committing the transaction.
///
public class UpdateRoleActorHandler(
    IUpdateRoleActor _updateRoleActor,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IUpdateRoleActorHandler
{
    ///
    /// Summary:
    ///     Updates an actor role and persists the staged changes.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     roleId:
    ///         The role identifier to update.
    ///
    ///     roleParams:
    ///         The role name, description and permission codes.
    ///
    /// Returns:
    ///     The updated role.
    ///
    /// Logic:
    ///     Delegates the owned actor role change and saves the staged changes once.
    ///     Invalidates the actor and parent permission caches after a successful save.
    ///
    public async Task<Role> ExecuteAsync(
        Guid profileId,
        Guid roleId,
        ActorObject.RoleParams roleParams
    )
    {
        Role role = await _updateRoleActor.ExecuteAsync(
            profileId,
            roleId,
            roleParams
        );
        await _unitOfWork.SyncAsync();
        await _permissionCachePort.RemoveAsync(role.AccountId);
        return role;
    }
}
