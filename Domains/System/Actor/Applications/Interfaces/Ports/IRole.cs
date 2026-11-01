namespace Wks.Ports;

///
/// Summary:
///     Port the actor module owns to reuse role capabilities from the role module.
///     Covers listing, creation, update, removal and permission metadata.
///
public interface IRoleActorPort
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
    Task<List<Role>> ListAsync(Guid accountId);

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
    Task<Role> CreateAsync(RoleObject.CreateParams createParams);

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
    Task<Role> UpdateAsync(
        Guid profileId,
        Guid roleId,
        RoleObject.UpdateParams updateParams
    );

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
    Task<IEnumerable<Role>> RemoveAsync(RoleObject.RemoveParams removeParams);

    ///
    /// Summary:
    ///     Lists the master permission definitions available for roles.
    ///
    /// Returns:
    ///     The master permission metadata entries.
    ///
    Task<List<RoleObject.PermissionMaster>> PermissionsAsync();
}
