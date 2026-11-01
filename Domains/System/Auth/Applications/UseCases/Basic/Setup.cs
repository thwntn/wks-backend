namespace Wks.UseCases;
///
/// Summary:
///     Provisions the default account role using platform settings and invalidates the setup cache.
///     Dependencies provide account lookup, settings, role/permission persistence and cache access.
///
public class SetupAccountAuth(
    IAccountRepository _accountRepository,
    ISettingRepository _settingRepository,
    IRoleRepository _roleRepository,
    IPermissionRepository _permissionRepository,
    ICache _cache
) : ISetupAccountAuth
{
    ///
    /// Summary:
    ///     Validates the account, creates its default role and permissions, then clears its setup cache key.
    ///     Throws HttpException with NotFound when accountId does not identify an account.
    ///     Persistence is committed by the calling setup handler.
    ///
    public async Task ExecuteAsync(Guid accountId)
    {
        if (!await _accountRepository.Raw.AnyAsync(account => account.Id == accountId))
            throw new HttpException(HttpStatus.NotFound, MessageConstant.NOT_FOUND_ACCOUNT);
        await Role(accountId);
        await _cache.RemoveAsync(Format.ToCacheName(nameof(CacheNames.AUTH_USER_SETUP), accountId));
    }
    ///
    /// Summary:
    ///     Creates the default role for accountId using codes stored in DEFAULT_PERMISSION.
    ///     Throws HttpException with BadRequest when the default permission setting is missing.
    ///
    private async Task Role(Guid accountId)
    {
        Setting setting =
            await _settingRepository.FirstOrDefaultAsync(setting =>
                setting.Name == nameof(SettingNames.DEFAULT_PERMISSION)
            )
            ?? throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.NOT_FOUND_SETTING
            );

        /// Logic:
        ///     Parse setting value as role definition with permission codes.
        ///
        List<string> codes = setting.Value.Deserialize<List<string>>();
        Role role = new(
            string.Empty,
            Format.ToConstant(nameof(SetupAccountAuth))
        )
        {
            AccountId = accountId,
            IsDefault = true,
        };

        _roleRepository.Create(role);
        _permissionRepository.Create(
            codes.Select(code => new Permission()
            {
                RoleId = role.Id,
                Code = code,
            })
        );
    }

    ///
    /// Summary:
    ///     Creates the default redirect domain for the user profile.
    ///     The host is read from APP_REDIRECT_DOMAIN; shop domains are configured explicitly.
    ///
    /// Parameters:
    ///     profileId:
    ///         The unique identifier of the profile to setup domains for.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when a required domain setting is not found.
    ///
}
