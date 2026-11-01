namespace Wks.UseCases;

///
/// Summary:
///     Use case for removing a role granted to an owned actor.
///
public class RemoveRoleActor(
    IRoleRepository _roleRepository,
    IProfileRepository _profileRepository,
    IRoleActorPort _roleActorPort
) : IRemoveRoleActor
{
    ///
    /// Summary:
    ///     Removes a role that belongs to an actor owned by the parent profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     roleId:
    ///         The role identifier to remove.
    ///
    /// Returns:
    ///     The removed roles.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the role does not belong to an owned actor.
    ///
    /// Logic:
    ///     Verifies the role is owned by an actor of the parent account.
    ///     Delegates removal to the role port.
    ///
    public async Task<IEnumerable<Role>> ExecuteAsync(
        Guid profileId,
        Guid roleId
    )
    {
        await EnsureRoleOwned(profileId, roleId);

        RoleObject.RemoveParams removeParams = new() { Ids = [roleId] };
        return await _roleActorPort.RemoveAsync(removeParams);
    }

    ///
    /// Summary:
    ///     Verifies the role belongs to an actor owned by the parent of the profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     roleId:
    ///         The role identifier to verify.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the role does not belong to an owned actor.
    ///
    private async Task EnsureRoleOwned(Guid profileId, Guid roleId)
    {
        Profile parent =
            await _profileRepository.Raw.FirstOrDefaultAsync(profile =>
                profile.Id == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        bool owned = await _roleRepository.Raw.AnyAsync(role =>
            role.Id == roleId
            && role.DeletedAt.HasValue == false
            && role.Account.AccountType == AccountNames.Actor
            && role.Account.Identifier == parent.AccountId
        );

        if (owned == false)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ROLE
            );
    }
}
