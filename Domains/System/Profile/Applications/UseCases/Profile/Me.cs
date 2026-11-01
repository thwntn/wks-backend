namespace Wks.UseCases;

public class MeProfile(DatabaseContext _databaseContext) : IMeProfile
{
    public async Task<Account> ExecuteAsync(Guid profileId)
    {
        Account account =
            await _databaseContext
                .Account.Include(account => account.Profile)
                .Include(account => account.Roles)
                    .ThenInclude(role => role.Permissions)
                .Include(account => account.LoginAccounts)
                .FirstOrDefaultAsync(account => account.Profile.Id == profileId)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        return account;
    }
}
