namespace Wks.UseCases;

///
/// Summary:
///     Use case for toggling the lock status of a sub-account (actor).
///     Switches the account between Open and Lock states.
///
public class StatusActor(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository
) : IStatusActor
{
    ///
    /// Summary:
    ///     Toggles an owned actor account between Open and Lock status.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier to toggle.
    ///
    /// Returns:
    ///     The actor account with the updated status.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the actor is not found or its status cannot be toggled.
    ///
    /// Logic:
    ///     Resolves the owned actor account from the parent profile.
    ///     Toggles Open and Lock; rejects accounts in any other status.
    ///
    public async Task<Account> ExecuteAsync(Guid profileId, Guid actorId)
    {
        Account actor = await ResolveActor(profileId, actorId);

        if (
            actor.AccountStatus is not AccountStatus.Open
            && actor.AccountStatus is not AccountStatus.Lock
        )
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.CANNOT_ACTION
            );

        actor.AccountStatus =
            actor.AccountStatus is AccountStatus.Lock
                ? AccountStatus.Open
                : AccountStatus.Lock;

        return _accountRepository.Update(actor);
    }

    ///
    /// Summary:
    ///     Resolves an actor account owned by the parent of the given profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier.
    ///
    /// Returns:
    ///     The owned actor account including its profile.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the actor is not found under the parent account.
    ///
    private async Task<Account> ResolveActor(Guid profileId, Guid actorId)
    {
        Profile parent =
            await _profileRepository.Raw.FirstOrDefaultAsync(profile =>
                profile.Id == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        return await _accountRepository
                .Raw.Include(account => account.Profile)
                .FirstOrDefaultAsync(account =>
                    account.Id == actorId
                    && account.AccountType == AccountNames.Actor
                    && account.Identifier == parent.AccountId
                )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );
    }
}
