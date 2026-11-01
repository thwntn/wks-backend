namespace Wks.Controllers;

///
/// Summary:
///     Provides administrator-only version endpoints.
///
[ApiController, Route(RouteConstant.ADMIN), Tags(SwaggerConstant.ADMIN)]
[Permission(nameof(PermissionConstant.ADMIN_SETTING))]
public class VersionAdminController(
    ICreateFeatureVersionAdminHandler _createFeatureVersion,
    ICreateVersionAdminHandler _createVersion,
    IRemoveFeatureVersionAdminHandler _removeFeatureVersion,
    IRemoveVersionAdminHandler _removeVersion,
    IUpdateVersionAdminHandler _updateVersion
) : ControllerBase
{
    ///
    /// Summary:
    ///     Creates version records through the administrator workflow.
    /// Parameters:
    ///     body: The validated HTTP request data mapped to the domain contract.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPost(RouteConstant.ADMIN_VERSION_CREATE)
    ]
    public async Task<IActionResult> Create(
        [FromBody] VersionDataTransformer.Create body
    )
    {
        AppVersion appVersion = await _createVersion.ExecuteAsync(
            body.MapTo<VersionObject.CreateParams>()
        );
        return Ok(appVersion);
    }

    ///
    /// Summary:
    ///     Updates version records through the administrator workflow.
    /// Parameters:
    ///     id: The selected resource identifier.
    ///     body: The validated HTTP request data mapped to the domain contract.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPatch(RouteConstant.ADMIN_VERSION_UPDATE)
    ]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] VersionDataTransformer.Update body
    )
    {
        AppVersion appVersion = await _updateVersion.ExecuteAsync(
            id,
            body.MapTo<VersionObject.UpdateParams>()
        );
        return Ok(appVersion);
    }

    ///
    /// Summary:
    ///     Soft-deletes version records through the administrator workflow.
    /// Parameters:
    ///     id: The selected resource identifier.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpDelete(RouteConstant.ADMIN_VERSION_REMOVE)
    ]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        await _removeVersion.ExecuteAsync(id);
        return Ok();
    }

    ///
    /// Summary:
    ///     Adds a changelog entry to version records through the administrator workflow.
    /// Parameters:
    ///     id: The selected resource identifier.
    ///     body: The validated HTTP request data mapped to the domain contract.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPost(RouteConstant.ADMIN_VERSION_CREATE_FEATURE)
    ]
    public async Task<IActionResult> CreateFeature(
        [FromRoute] Guid id,
        [FromBody] VersionDataTransformer.CreateFeature body
    )
    {
        AppFeature feature = await _createFeatureVersion.ExecuteAsync(
            id,
            body.MapTo<VersionObject.CreateFeatureParams>()
        );
        return Ok(feature);
    }

    ///
    /// Summary:
    ///     Removes a changelog entry from version records through the administrator workflow.
    /// Parameters:
    ///     featureId: The changelog entry to remove.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpDelete(RouteConstant.ADMIN_VERSION_REMOVE_FEATURE)
    ]
    public async Task<IActionResult> RemoveFeature([FromRoute] Guid featureId)
    {
        await _removeFeatureVersion.ExecuteAsync(featureId);
        return Ok();
    }
}
