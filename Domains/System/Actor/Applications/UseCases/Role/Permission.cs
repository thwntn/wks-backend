namespace Wks.UseCases;

///
/// Summary:
///     Use case for listing the master permission definitions available to actors.
///
public class PermissionActor(IRoleActorPort _roleActorPort) : IPermissionActor
{
    ///
    /// Summary:
    ///     Lists the master permission definitions used when granting actor roles.
    ///
    /// Returns:
    ///     The master permission metadata entries.
    ///
    /// Logic:
    ///     Delegates retrieval to the role port.
    ///
    public async Task<List<RoleObject.PermissionMaster>> ExecuteAsync() =>
        await _roleActorPort.PermissionsAsync();
}
