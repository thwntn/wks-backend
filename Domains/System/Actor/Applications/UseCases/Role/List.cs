namespace Wks.UseCases;

///
/// Summary:
///     Use case for listing the roles granted to an owned sub-account (actor).
///
public class ListRoleActor(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository,
    IRoleActorPort _roleActorPort
) : IListRoleActor
{
    ///
    /// Summary:
    ///     Lists the roles of an actor owned by the parent of the given profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier whose roles are listed.
    ///
    /// Returns:
    ///     The roles granted to the actor account.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the actor is not found under the parent account.
    ///
    /// Logic:
    ///     Verifies the actor belongs to the parent account.
    ///     Delegates listing to the role port keyed by the actor account id.
    ///
    public async Task<List<Role>> ExecuteAsync(Guid profileId, Guid actorId)
    {
        await EnsureActorOwned(profileId, actorId);
        return await _roleActorPort.ListAsync(actorId);
    }

    ///
    /// Summary:
    ///     Verifies that the actor account is owned by the parent of the given profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier to verify.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the actor is not found under the parent account.
    ///
    private async Task EnsureActorOwned(Guid profileId, Guid actorId)
    {
        Profile parent =
            await _profileRepository.Raw.FirstOrDefaultAsync(profile =>
                profile.Id == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        bool owned = await _accountRepository.Raw.AnyAsync(account =>
            account.Id == actorId
            && account.AccountType == AccountNames.Actor
            && account.Identifier == parent.AccountId
        );

        if (owned == false)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );
    }
}
