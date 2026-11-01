namespace Wks.UseCases;

public class UpdateProfile(IAccountRepository _accountRepository)
    : IUpdateProfile
{
    public Account Execute(Guid profileId, ProfileDataTransformer.Update update)
    {
        Account account =
            _accountRepository
                .Raw.Include(account => account.Profile)
                .FirstOrDefault(account => account.Profile.Id == profileId)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        account.Profile = account.Profile.Merge(update);
        _accountRepository.Update(account);

        return account;
    }
}
