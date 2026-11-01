namespace Wks.UseCases;

///
/// Summary:
///     Ensures the current account has ADMIN_SETTING permission via an assigned role.
///
public class EnsureAdminSetting(IAccountRepository _accountRepository)
    : IEnsureAdminSetting
{
    public async Task ExecuteAsync(Guid profileId)
    {
        Account account =
            await _accountRepository
                .Include(account => account.Profile)
                .Include(account => account.Roles)
                    .ThenInclude(role => role.Permissions)
                .FirstOrDefaultAsync(account => account.Profile.Id == profileId)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        string permissionCode = nameof(PermissionConstant.ADMIN_SETTING);

        bool hasPermission =
            account.Roles?.Any(role =>
                role.Permissions?.Any(permission =>
                    permission.Code == permissionCode
                ) == true
            ) == true;

        if (!hasPermission)
            throw new HttpException(
                HttpStatus.Forbidden,
                MessageConstant.NOT_ACCEPT_ROLE
            );
    }
}
