namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to CreateRole.
///
public class CreateRoleAdminAdapter(ICreateRole _capability)
    : ICreateRoleAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     createParams: The allowed fields for the new resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Role> ExecuteAsync(RoleObject.CreateParams createParams) =>
        _capability.ExecuteAsync(createParams);
}
