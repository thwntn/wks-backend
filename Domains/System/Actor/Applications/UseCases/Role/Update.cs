namespace Wks.UseCases;

///
/// Summary:
///     Use case for updating a role granted to an owned actor.
///
public class UpdateRoleActor(
    IRoleRepository _roleRepository,
    IProfileRepository _profileRepository,
    IRoleActorPort _roleActorPort
) : IUpdateRoleActor
{
    ///
    /// Summary:
    ///     Updates a role that belongs to an actor owned by the parent profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     roleId:
    ///         The role identifier to update.
    ///
    ///     roleParams:
    ///         The role name, description and permission codes.
    ///
    /// Returns:
    ///     The updated role.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the role does not belong to an owned actor.
    ///
    /// Logic:
    ///     Verifies the role is owned by an actor of the parent account.
    ///     Builds the update parameters and delegates to the role port.
    ///
    public async Task<Role> ExecuteAsync(
        Guid profileId,
        Guid roleId,
        ActorObject.RoleParams roleParams
    )
    {
        await EnsureRoleOwned(profileId, roleId);

        RoleObject.UpdateParams updateParams = new()
        {
            Name = roleParams.Name,
            Description = roleParams.Description,
            Permissions = roleParams.Permissions,
        };

        return await _roleActorPort.UpdateAsync(profileId, roleId, updateParams);
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
