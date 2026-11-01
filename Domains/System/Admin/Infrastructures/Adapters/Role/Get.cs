namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to GetRole.
///
public class GetRoleAdminAdapter(IGetRole _capability) : IGetRoleAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     roleId: The role selected for the authorization change.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Role> ExecuteAsync(Guid roleId) =>
        _capability.ExecuteAsync(roleId);
}
