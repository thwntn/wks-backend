namespace Wks.UseCases;

///
/// Summary:
///     Use case for granting a new role (with permissions) to an owned actor.
///
public class CreateRoleActor(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository,
    IRoleActorPort _roleActorPort
) : ICreateRoleActor
{
    ///
    /// Summary:
    ///     Creates a role for an actor owned by the parent of the given profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier the role is created for.
    ///
    ///     roleParams:
    ///         The role name, code, description and permission codes.
    ///
    /// Returns:
    ///     The created role.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the actor is not found under the parent account.
    ///
    /// Logic:
    ///     Verifies the actor belongs to the parent account.
    ///     Builds the role creation parameters bound to the actor account.
    ///     Delegates creation to the role port.
    ///
    public async Task<Role> ExecuteAsync(
        Guid profileId,
        Guid actorId,
        ActorObject.RoleParams roleParams
    )
    {
        await EnsureActorOwned(profileId, actorId);

        RoleObject.CreateParams createParams = new()
        {
            AccountId = actorId,
            Name = roleParams.Name,
            Code = roleParams.Code,
            Description = roleParams.Description,
            Permissions = roleParams.Permissions,
        };

        return await _roleActorPort.CreateAsync(createParams);
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
