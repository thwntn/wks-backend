namespace Wks.Controllers;

///
/// Summary:
///     Provides administrator-only domain endpoints.
///
[ApiController, Route(RouteConstant.ADMIN), Tags(SwaggerConstant.ADMIN)]
[Permission(nameof(PermissionConstant.ADMIN_SETTING))]
public class DomainAdminController(
    IRemoveDomainAdminHandler _removeDomainHandler,
    ITypeDomainAdminPort _typeDomain,
    IUpdateDomainAdminHandler _updateDomainHandler
) : ControllerBase
{
    ///
    /// Summary:
    ///     Lists routing types for domain records through the administrator workflow.
    /// Parameters:
    ///     None.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_DOMAIN_TYPE)
    ]
    public async Task<IActionResult> Type()
    {
        List<DomainObject.Type> types = await _typeDomain.ExecuteAsync();
        return Ok(types);
    }

    ///
    /// Summary:
    ///     Updates domain records through the administrator workflow.
    /// Parameters:
    ///     profileId: The profile whose records are read or changed.
    ///     update: The fields allowed when updating this resource.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPut(RouteConstant.ADMIN_DOMAIN_UPDATE)
    ]
    public async Task<IActionResult> Update(
        [FromRoute] Guid profileId,
        [FromBody] DomainDataTransformer.Update update
    )
    {
        Guid identifier = profileId;
        Domain domain = await _updateDomainHandler.ExecuteAsync(
            identifier,
            update
        );
        return Ok(domain);
    }

    ///
    /// Summary:
    ///     Soft-deletes domain records through the administrator workflow.
    /// Parameters:
    ///     profileId: The profile whose records are read or changed.
    ///     remove: The identifiers selected for soft deletion.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpDelete(RouteConstant.ADMIN_DOMAIN_REMOVE)
    ]
    public async Task<IActionResult> Remove(
        [FromRoute] Guid profileId,
        [FromBody] DomainDataTransformer.Remove remove
    )
    {
        Guid identifier = profileId;
        string message = await _removeDomainHandler.ExecuteAsync(
            identifier,
            remove.Ids
        );
        return Ok(message);
    }
}
