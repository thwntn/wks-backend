namespace Wks.UseCases;

///
/// Summary:
///     Use case for resolving request profile scope from a token account ID.
///
public class ResolveIdentityToken(IAccountRepository _accountRepository)
    : IResolveIdentityToken
{
    ///
    /// Summary:
    ///     Resolves the profile required by a personal or business endpoint.
    ///
    /// Parameters:
    ///     accountId:
    ///         The original account ID carried by the token.
    ///
    ///     personal:
    ///         True to retain the actor own profile; false to use its parent business profile.
    ///
    /// Returns:
    ///     The selected non-deleted profile ID.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the subject, parent link or required profile is missing.
    ///
    /// Logic:
    ///     Reads the subject account from the database without using token type hints.
    ///     Resolves its parent for actor business requests and returns the selected profile.
    ///
    public async Task<Guid> ExecuteAsync(Guid accountId, bool personal = false)
    {
        Account account = await GetAccount(accountId);
        if (personal == false && account.AccountType is AccountNames.Actor)
            account = await GetParent(account);

        return account.Profile.Id;
    }

    ///
    /// Summary:
    ///     Retrieves an account with its required profile.
    ///
    /// Parameters:
    ///     accountId:
    ///         The account primary key to resolve.
    ///
    /// Returns:
    ///     The account with a non-deleted profile.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the account or profile is missing or deleted.
    ///
    /// Logic:
    ///     Filters soft-deleted records and loads the profile relationship.
    ///     Guards the materialized profile before returning the account.
    ///
    private async Task<Account> GetAccount(Guid accountId)
    {
        Account account =
            await _accountRepository
                .Raw.Include(account => account.Profile)
                .FirstOrDefaultAsync(account =>
                    account.Id == accountId
                    && account.DeletedAt.HasValue == false
                    && account.Profile.DeletedAt.HasValue == false
                )
            ?? throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.NOT_FOUND_ACCOUNT
            );
        if (account.Profile.Justifiable() is false)
            throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        return account;
    }

    ///
    /// Summary:
    ///     Resolves the parent account linked to the actor being processed.
    ///
    /// Parameters:
    ///     actor:
    ///         The actor account whose Identifier stores its parent account ID.
    ///
    /// Returns:
    ///     The linked parent account with its profile.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the actor parent link or parent profile is unavailable.
    ///
    /// Logic:
    ///     Guards the materialized nullable link before loading its account.
    ///
    private async Task<Account> GetParent(Account actor)
    {
        if (actor.Identifier.Justifiable() is false)
            throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        return await GetAccount(actor.Identifier.Value);
    }
}
