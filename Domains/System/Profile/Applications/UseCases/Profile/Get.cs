namespace Wks.UseCases;

public class GetProfile(IProfileRepository _profileRepository) : IGetProfile
{
    public async Task<Profile> ExecuteAsync(Guid profileId) =>
        await _profileRepository
            .Include(profile => profile.Account)
                .ThenInclude(account => account.Roles)
                    .ThenInclude(role => role.Permissions)
            .FirstOrDefaultAsync(profile => profile.Id == profileId)
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_ACCOUNT
        );
}
