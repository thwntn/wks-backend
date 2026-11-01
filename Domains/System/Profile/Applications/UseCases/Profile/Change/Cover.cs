namespace Wks.UseCases;

public class ChangeCoverPictureProfile(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository
) : IChangeCoverPictureProfile
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

        account.Profile.CoverPicture = artifactToken;
        _profileRepository.Update(account.Profile);

        return account;
    }
}
