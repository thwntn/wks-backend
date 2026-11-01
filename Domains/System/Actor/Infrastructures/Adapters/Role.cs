namespace Wks.Adapters;

///
/// Summary:
///     Adapter binding the actor role port to the role module use cases.
///     Delegates each operation to the owning role capability without committing.
///
public class RoleActorAdapter(
    IListRoles _listRoles,
    ICreateRole _createRole,
    IUpdateRole _updateRole,
    IRemoveRole _removeRole,
    IPermissionRole _permissionRole
) : IRoleActorPort
{
    ///
    /// Summary:
    ///     Lists the roles owned by an account.
    ///
    /// Parameters:
    ///     accountId:
    ///         The account whose roles are listed.
    ///
    /// Returns:
    ///     The roles belonging to the account.
    ///
    public Task<List<Role>> ListAsync(Guid accountId) =>
        _listRoles.ExecuteAsync(accountId);

    ///
    /// Summary:
    ///     Creates a role with its permissions for an account.
    ///
    /// Parameters:
    ///     createParams:
    ///         The role creation parameters.
    ///
    /// Returns:
    ///     The created role.
    ///
    public Task<Role> CreateAsync(RoleObject.CreateParams createParams) =>
        _createRole.ExecuteAsync(createParams);

    ///
    /// Summary:
    ///     Updates a role and replaces its permissions.
    ///
    /// Parameters:
    ///     profileId:
    ///         The acting profile identifier.
    ///
    ///     roleId:
    ///         The role identifier to update.
    ///
    ///     updateParams:
    ///         The role update parameters.
    ///
    /// Returns:
    ///     The updated role.
    ///
    public Task<Role> UpdateAsync(
        Guid profileId,
        Guid roleId,
        RoleObject.UpdateParams updateParams
    ) => _updateRole.ExecuteAsync(profileId, roleId, updateParams);

    ///
    /// Summary:
    ///     Removes roles by their identifiers.
    ///
    /// Parameters:
    ///     removeParams:
    ///         The identifiers of the roles to remove.
    ///
    /// Returns:
    ///     The removed roles.
    ///
    public Task<IEnumerable<Role>> RemoveAsync(
        RoleObject.RemoveParams removeParams
    ) => _removeRole.ExecuteAsync(removeParams);

    ///
    /// Summary:
    ///     Lists the master permission definitions available for roles.
    ///
    /// Returns:
    ///     The master permission metadata entries.
    ///
    public Task<List<RoleObject.PermissionMaster>> PermissionsAsync() =>
        _permissionRole.ExecuteAsync();
}
