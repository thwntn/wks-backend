namespace Wks.Controllers;

///
/// Summary:
///     Provides administrator-only profile endpoints.
///
[ApiController, Route(RouteConstant.ADMIN), Tags(SwaggerConstant.ADMIN)]
[Permission(nameof(PermissionConstant.ADMIN_SETTING))]
public class ProfileAdminController(
    IGetProfileAdminPort _getProfile,
    IListProfileAdminPort _listProfile
) : ControllerBase
{
    ///
    /// Summary:
    ///     Lists profile records through the administrator workflow.
    ///
    /// Parameters:
    ///     queryOptions:
    ///         The search, sort and pagination options for the profile list.
    ///
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    ///
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    /// Logic:
    ///     Maps the allowed request fields to the action contract.
    ///     Invokes _listProfile.ExecuteAsync.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_PROFILE_LIST)
    ]
    public async Task<IActionResult> List([FromQuery] QueryOptions queryOptions)
    {
        Paged<Profile> profiles = await _listProfile.ExecuteAsync(queryOptions);
        Paged<AuthObject.ProfileResponse> response = profiles.MapTo<
            Paged<AuthObject.ProfileResponse>
        >();
        return Ok(response);
    }

    ///
    /// Summary:
    ///     Retrieves profile records through the administrator workflow.
    ///
    /// Parameters:
    ///     id:
    ///         The selected resource identifier.
    ///
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    ///
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    /// Logic:
    ///     Maps the allowed request fields to the action contract.
    ///     Invokes _getProfile.ExecuteAsync.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_PROFILE_GET)
    ]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        Profile profile = await _getProfile.ExecuteAsync(id);
        AuthObject.ProfileResponse profileResponse =
            profile.MapTo<AuthObject.ProfileResponse>();
        return Ok(profileResponse);
    }
}
