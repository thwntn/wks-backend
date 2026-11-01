namespace Wks.UseCases;

///
/// Summary:
///     Use case for updating a sub-account (actor) profile owned by the parent.
///
public class UpdateActor(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository
) : IUpdateActor
{
    ///
    /// Summary:
    ///     Updates the profile name and description of an owned actor account.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier to update.
    ///
    ///     updateParams:
    ///         The update parameters carrying name and description.
    ///
    /// Returns:
    ///     The updated actor account including its profile.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the actor is not found under the parent account.
    ///
    /// Logic:
    ///     Resolves the owned actor account from the parent profile.
    ///     Updates the actor profile name and description.
    ///
    public async Task<Account> ExecuteAsync(
        Guid profileId,
        Guid actorId,
        ActorObject.UpdateParams updateParams
    )
    {
        Account actor = await ResolveActor(profileId, actorId);

        actor.Profile.Name = updateParams.Name;
        actor.Profile.Description = updateParams.Description;
        _profileRepository.Update(actor.Profile);

        return actor;
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
