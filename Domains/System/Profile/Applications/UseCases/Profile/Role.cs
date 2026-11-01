namespace Wks.UseCases;

public class GetAccountWithRoleProfile(IAccountRepository _accountRepository)
    : IGetAccountWithRoleProfile
{
    public Account Execute(Guid accountId) =>
        _accountRepository
            .Include(account => account.Profile)
            .FirstOrDefault(account => account.Id == accountId)
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_ACCOUNT
        );
}
