namespace Wks.UseCases;

///
/// Summary:
///     Stages domain setup for an account selected by an administrator.
///
public class SetupDomainAdmin(
    IProfileRepository _profileRepository,
    IDomainAdminPort _domainPort
) : ISetupDomainAdmin
{
    ///
    /// Summary:
    ///     Resolves the selected account profile before staging domain creation.
    /// Logic:
    ///     Requires a non-deleted profile and account, then applies existing domain rules.
    /// Parameters:
    ///     accountId: The destination account identifier.
    ///     create: The requested domain host and routing type.
    /// Returns:
    ///     The staged domain; the application handler persists it.
    /// Exceptions:
    ///     HttpException when the profile is unavailable or domain creation fails.
    ///
    public async Task<Domain> ExecuteAsync(
        Guid accountId,
        DomainAdminObject.SetupParams create
    )
    {
        Profile profile =
            await _profileRepository.Raw.FirstOrDefaultAsync(profile =>
                profile.AccountId == accountId
                && profile.DeletedAt.HasValue == false
                && profile.Account.DeletedAt.HasValue == false
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_PROFILE
            );

        return await _domainPort.ExecuteAsync(profile.Id, create);
    }
}
