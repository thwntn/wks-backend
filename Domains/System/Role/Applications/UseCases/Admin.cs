namespace Wks.UseCases;

///
/// Summary:
///     Replaces the target account's roles with a default role granting every permission.
///
public class MakeAdmin(
    IRoleRepository _roleRepository,
    IPermissionRepository _permissionRepository
) : IMakeAdmin
{
    ///
    /// Summary:
    ///     Removes all existing roles and permissions before staging a fresh default role.
    /// Parameters:
    ///     accountId:
    ///         The account whose authorization records are replaced.
    /// Returns:
    ///     The new default role with every PerrmissionContant permission staged for persistence.
    ///
    public async Task<Role> ExecuteAsync(Guid accountId)
    {
        await RemoveAuthorization(accountId);
        return CreateDefaultRole(accountId);
    }

    ///
    /// Summary:
    ///     Stages authorization records for deletion, including soft-deleted records.
    /// Parameters:
    ///     accountId:
    ///         The account that owns the roles and their permissions.
    ///
    private async Task RemoveAuthorization(Guid accountId)
    {
        List<Permission> permissions = await _permissionRepository
            .Raw.AsTracking()
            .Where(permission => permission.Role.AccountId == accountId)
            .ToListAsync();
        List<Role> roles = await _roleRepository
            .Raw.AsTracking()
            .Where(role => role.AccountId == accountId)
            .ToListAsync();
        _permissionRepository.Remove(permissions);
        _roleRepository.Remove(roles);
    }

    ///
    /// Summary:
    ///     Stages the signup default role with the complete system permission set.
    /// Parameters:
    ///     accountId:
    ///         The account receiving the default role.
    /// Returns:
    ///     The newly created role.
    ///
    private Role CreateDefaultRole(Guid accountId)
    {
        string code = Format.ToConstant(nameof(SetupAccountAuth));
        Role role = new(string.Empty, code) { AccountId = accountId };
        _roleRepository.Create(role);
        _permissionRepository.Create(
            Enum.GetNames<PermissionConstant>()
                .Select(code => new Permission
                {
                    RoleId = role.Id,
                    Code = code,
                })
        );
        return role;
    }
}
