namespace Wks.UseCases;

public class ChangeAvatarProfile(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository
) : IChangeAvatarProfile
{
    public async Task<Account> ExecuteAsync(
        Guid profileId,
        string artifactToken
    )
    {
        Account account =
            await _accountRepository
                .Raw.Include(account => account.Profile)
                .FirstOrDefaultAsync(account => account.Profile.Id == profileId)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        account.Profile.Avatar = artifactToken;
        _profileRepository.Update(account.Profile);

        return account;
    }
}
