namespace Wks.UseCases;

///
/// Summary:
///     Replaces non-admin account permissions with the configured default permissions.
///
public class SyncFeaturesAdmin(
    IGetSetting _getSetting,
    IAccountRepository _accountRepository,
    IRoleRepository _roleRepository,
    IPermissionRepository _permissionRepository
) : ISyncFeaturesAdmin
{
    ///
    /// Summary:
    ///     Validates settings before replacing permissions and ensuring default roles.
    /// Returns:
    ///     The affected account identifiers for cache invalidation after commit.
    /// Exceptions:
    ///     HttpException when DEFAULT_PERMISSION is missing or invalid.
    ///
    public async Task<List<Guid>> ExecuteAsync()
    {
        List<string> codes = await GetDefaultPermissions();
        List<Guid> accountIds = await GetAccounts();
        await RemovePermissions(accountIds);
        await AssignDefaultPermissions(accountIds, codes);
        return accountIds;
    }

    ///
    /// Summary:
    ///     Loads the current remote setting without using cached values.
    /// Returns:
    ///     Distinct, valid non-admin permission codes.
    ///
    private async Task<List<string>> GetDefaultPermissions()
    {
        string settingName = nameof(SettingNames.DEFAULT_PERMISSION);
        Dictionary<string, string> settings = await _getSetting.ExecuteAsync([
            settingName,
        ]);

        if (settings.TryGetValue(settingName, out string value) == false)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.NOT_FOUND_SETTING
            );

        List<string> codes = value.Deserialize<List<string>>();
        if (codes.Any(code => code == nameof(PermissionConstant.ADMIN_SETTING)))
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.INVALID_DEFAULT_PERMISSION
            );

        return codes.Distinct().ToList();
    }

    ///
    /// Summary:
    ///     Selects active accounts without admin permission on any active role.
    /// Returns:
    ///     The accounts eligible for synchronization.
    ///
    private Task<List<Guid>> GetAccounts() =>
        _accountRepository
            .Raw.AsNoTracking()
            .Where(account =>
                account.Roles.Any(role =>
                    role.Permissions.Any(permission =>
                        permission.Code
                        == nameof(PermissionConstant.ADMIN_SETTING)
                    )
                ) == false
            )
            .Select(account => account.Id)
            .ToListAsync();

    ///
    /// Summary:
    ///     Deletes all permission rows belonging to the selected accounts, including old roles.
    /// Parameters:
    ///     accountIds: The non-admin accounts selected before mutation.
    ///
    private async Task RemovePermissions(List<Guid> accountIds)
    {
        List<Permission> permissions = await _permissionRepository
            .Raw.AsTracking()
            .Where(permission => accountIds.Contains(permission.Role.AccountId))
            .ToListAsync();
        _permissionRepository.Remove(permissions);
    }

    ///
    /// Summary:
    ///     Reuses the signup default role or creates it and stages configured permissions.
    /// Parameters:
    ///     accountIds: The accounts to update.
    ///     codes: The validated DEFAULT_PERMISSION codes.
    ///
    private async Task AssignDefaultPermissions(
        List<Guid> accountIds,
        List<string> codes
    )
    {
        string defaultCode = Format.ToConstant(nameof(SetupAccountAuth));

        List<Role> roles = await _roleRepository
            .Raw.AsTracking()
            .Where(role =>
                accountIds.Contains(role.AccountId)
                && role.Code == defaultCode
                && role.DeletedAt.HasValue == false
            )
            .OrderBy(role => role.CreatedAt)
            .ThenBy(role => role.Id)
            .ToListAsync();

        foreach (Guid accountId in accountIds)
        {
            Role role = roles.FirstOrDefault(role =>
                role.AccountId == accountId
            );

            if (role.Justifiable() is false)
                role = _roleRepository.Create(
                    new Role(string.Empty, defaultCode)
                    {
                        AccountId = accountId,
                    }
                );

            _permissionRepository.Create(
                codes.Select(code => new Permission
                {
                    RoleId = role.Id,
                    Code = code,
                })
            );
        }
    }
}
