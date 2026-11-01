namespace Wks.UseCases;

///
/// Summary:
///     Use case for listing the sub-accounts (actors) of a parent account.
///
public class ListActor(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository
) : IListActor
{
    ///
    /// Summary:
    ///     Lists the actor accounts belonging to the parent of the given profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     queryOptions:
    ///         Pagination, search and sort options.
    ///
    /// Returns:
    ///     A paginated list of actor accounts with their profiles and roles.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the parent profile is not found.
    ///
    /// Logic:
    ///     Resolves the parent account identifier from the profile.
    ///     Queries actor accounts linked to the parent account.
    ///     Returns a paginated result ordered by most recent.
    ///
    public async Task<Paged<Account>> ExecuteAsync(
        Guid profileId,
        QueryOptions queryOptions
    )
    {
        Profile profile =
            await _profileRepository.Raw.FirstOrDefaultAsync(profile =>
                profile.Id == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        IQueryable<Account> actors = _accountRepository
            .Include(account => account.Profile)
            .Include(account => account.Roles)
            .Where(account =>
                account.Identifier == profile.AccountId
                && account.AccountType == AccountNames.Actor
            )
            .OrderByDescending(account => account.CreatedAt);

        return await Pagination.CreateAsync(actors, queryOptions);
    }
}
