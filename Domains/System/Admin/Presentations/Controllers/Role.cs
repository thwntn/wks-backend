namespace Wks.Controllers;

///
/// Summary:
///     Provides administrator-only role endpoints.
///
[ApiController, Route(RouteConstant.ADMIN), Tags(SwaggerConstant.ADMIN)]
[Permission(nameof(PermissionConstant.ADMIN_SETTING))]
public class RoleAdminController(
    IAssignRoleAdminHandler _assignRoleHandler,
    ICreateRoleAdminHandler _createRoleHandler,
    IGetRoleAdminPort _getRole,
    IListRolesAdminPort _listRoles,
    IPermissionRoleAdminPort _permissionRole,
    IRemoveRoleAdminHandler _removeRoleHandler,
    IUnassignRoleAdminHandler _unassignRoleHandler,
    IUpdateRoleAdminHandler _updateRoleHandler
) : ControllerBase
{
    ///
    /// Summary:
    ///     Creates role records through the administrator workflow.
    /// Parameters:
    ///     body: The validated HTTP request data mapped to the domain contract.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPost(RouteConstant.ADMIN_ROLE_CREATE)
    ]
    public async Task<IActionResult> Create(
        [FromBody] RoleDataTransformer.Create body
    )
    {
        Role role = await _createRoleHandler.ExecuteAsync(
            body.MapTo<RoleObject.CreateParams>()
        );
        return Ok(role);
    }

    ///
    /// Summary:
    ///     Lists role records through the administrator workflow.
    /// Parameters:
    ///     accountId: The account whose roles or configuration are managed.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_ROLE_LIST)
    ]
    public async Task<IActionResult> List([FromRoute] Guid accountId)
    {
        List<Role> roles = await _listRoles.ExecuteAsync(accountId);
        return Ok(roles);
    }

    ///
    /// Summary:
    ///     Retrieves role records through the administrator workflow.
    /// Parameters:
    ///     id: The selected resource identifier.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_ROLE_GET)
    ]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        Role role = await _getRole.ExecuteAsync(id);
        return Ok(role);
    }

    ///
    /// Summary:
    ///     Updates role records through the administrator workflow.
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
        HttpPatch(RouteConstant.ADMIN_ROLE_UPDATE)
    ]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] RoleDataTransformer.Update body
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Role role = await _updateRoleHandler.ExecuteAsync(
            identifier,
            id,
            body.MapTo<RoleObject.UpdateParams>()
        );
        return Ok(role);
    }

    ///
    /// Summary:
    ///     Soft-deletes role records through the administrator workflow.
    /// Parameters:
    ///     body: The validated HTTP request data mapped to the domain contract.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpDelete(RouteConstant.ADMIN_ROLE_REMOVE)
    ]
    public async Task<IActionResult> Remove(
        [FromBody] RoleDataTransformer.Remove body
    )
    {
        IEnumerable<Role> roles = await _removeRoleHandler.ExecuteAsync(
            body.MapTo<RoleObject.RemoveParams>()
        );
        return Ok(roles);
    }

    ///
    /// Summary:
    ///     Assigns role records through the administrator workflow.
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
        HttpPost(RouteConstant.ADMIN_ROLE_ASSIGN)
    ]
    public async Task<IActionResult> Assign(
        [FromRoute] Guid id,
        [FromBody] RoleDataTransformer.Assign body
    )
    {
        Role role = await _assignRoleHandler.ExecuteAsync(
            id,
            body.MapTo<RoleObject.AssignParams>()
        );
        return Ok(role);
    }

    ///
    /// Summary:
    ///     Unassigns role records through the administrator workflow.
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
        HttpPost(RouteConstant.ADMIN_ROLE_UNASSIGN)
    ]
    public async Task<IActionResult> Unassign(
        [FromRoute] Guid id,
        [FromBody] RoleDataTransformer.Unassign body
    )
    {
        string message = await _unassignRoleHandler.ExecuteAsync(
            id,
            body.MapTo<RoleObject.UnassignParams>()
        );
        return Ok(message);
    }

    ///
    /// Summary:
    ///     Lists available permissions for role records through the administrator workflow.
    /// Parameters:
    ///     None.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_ROLE_PERMISSIONS)
    ]
    public async Task<IActionResult> Permissions()
    {
        List<RoleObject.PermissionMaster> permissions =
            await _permissionRole.ExecuteAsync();
        return Ok(permissions);
    }
}
