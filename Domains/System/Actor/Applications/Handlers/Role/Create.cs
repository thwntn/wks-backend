namespace Wks.Handlers;

///
/// Summary:
///     Handler for granting a role to an actor and committing the transaction.
///
public class CreateRoleActorHandler(
    ICreateRoleActor _createRoleActor,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : ICreateRoleActorHandler
{
    ///
    /// Summary:
    ///     Creates an actor role and persists the staged changes.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier the role is created for.
    ///
    ///     roleParams:
    ///         The role name, code, description and permission codes.
    ///
    /// Returns:
    ///     The created role.
    ///
    /// Logic:
    ///     Delegates the owned actor role change and saves the staged changes once.
    ///     Invalidates the actor and parent permission caches after a successful save.
    ///
    public async Task<Role> ExecuteAsync(
        Guid profileId,
        Guid actorId,
        ActorObject.RoleParams roleParams
    )
    {
        Role role = await _createRoleActor.ExecuteAsync(
            profileId,
            actorId,
            roleParams
        );
        await _unitOfWork.SyncAsync();
        await _permissionCachePort.RemoveAsync(role.AccountId);
        return role;
    }
}
