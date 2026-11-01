namespace Wks.Controllers;

///
/// Summary:
///     Provides HTTP API endpoints for managing sub-accounts (actors).
///     Supports creation, listing, update, status toggle and role assignment.
///
[
    ApiController,
    Route(RouteConstant.ACTOR),
    Tags(SwaggerConstant.ACTOR)
]
public class ActorController(
    ICreateActorHandler _createActorHandler,
    IListActor _listActor,
    IUpdateActorHandler _updateActorHandler,
    IStatusActorHandler _statusActorHandler,
    IListRoleActor _listRoleActor,
    ICreateRoleActorHandler _createRoleActorHandler,
    IUpdateRoleActorHandler _updateRoleActorHandler,
    IRemoveRoleActorHandler _removeRoleActorHandler,
    IPermissionActor _permissionActor
) : ControllerBase
{
    ///
    /// Summary:
    ///     Creates a new sub-account under the authenticated parent account.
    ///
    [Permission(nameof(PermissionConstant.AUTH_ACTOR)), HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ActorDataTransformer.Create body
    )
    {
        Guid identifier = await HttpContext.Identifier();
        ActorObject.CreateParams createParams =
            body.MapTo<ActorObject.CreateParams>();

        Account account = await _createActorHandler.ExecuteAsync(
            identifier,
            createParams
        );
        return Ok(account);
    }

    ///
    /// Summary:
    ///     Lists the sub-accounts owned by the authenticated parent account.
    ///
    [Permission(nameof(PermissionConstant.AUTH_ACTOR)), HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] QueryOptions queryOptions
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Paged<Account> accounts = await _listActor.ExecuteAsync(
            identifier,
            queryOptions
        );
        return Ok(accounts);
    }

    ///
    /// Summary:
    ///     Updates an owned sub-account profile.
    ///
    [Permission(nameof(PermissionConstant.AUTH_ACTOR)), HttpPatch(RouteConstant.ID)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] ActorDataTransformer.Update body
    )
    {
        Guid identifier = await HttpContext.Identifier();
        ActorObject.UpdateParams updateParams =
            body.MapTo<ActorObject.UpdateParams>();

        Account account = await _updateActorHandler.ExecuteAsync(
            identifier,
            id,
            updateParams
        );
        return Ok(account);
    }

    ///
    /// Summary:
    ///     Toggles the lock status of an owned sub-account.
    ///
    [
        Permission(nameof(PermissionConstant.AUTH_ACTOR)),
        HttpPost(RouteConstant.ACTOR_STATUS)
    ]
    public async Task<IActionResult> Status([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Account account = await _statusActorHandler.ExecuteAsync(
            identifier,
            id
        );
        return Ok(account);
    }

    ///
    /// Summary:
    ///     Lists the roles granted to an owned sub-account.
    ///
    [
        Permission(nameof(PermissionConstant.AUTH_ACTOR)),
        HttpGet(RouteConstant.ACTOR_ROLE_LIST)
    ]
    public async Task<IActionResult> ListRole([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        List<Role> roles = await _listRoleActor.ExecuteAsync(identifier, id);
        return Ok(roles);
    }

    ///
    /// Summary:
    ///     Grants a new role with permissions to an owned sub-account.
    ///
    [
        Permission(nameof(PermissionConstant.AUTH_ACTOR)),
        HttpPost(RouteConstant.ACTOR_ROLE_CREATE)
    ]
    public async Task<IActionResult> CreateRole(
        [FromRoute] Guid id,
        [FromBody] ActorDataTransformer.Role body
    )
    {
        Guid identifier = await HttpContext.Identifier();
        ActorObject.RoleParams roleParams =
            body.MapTo<ActorObject.RoleParams>();

        Role role = await _createRoleActorHandler.ExecuteAsync(
            identifier,
            id,
            roleParams
        );
        return Ok(role);
    }

    ///
    /// Summary:
    ///     Updates a role granted to an owned sub-account.
    ///
    [
        Permission(nameof(PermissionConstant.AUTH_ACTOR)),
        HttpPatch(RouteConstant.ACTOR_ROLE_UPDATE)
    ]
    public async Task<IActionResult> UpdateRole(
        [FromRoute] Guid roleId,
        [FromBody] ActorDataTransformer.Role body
    )
    {
        Guid identifier = await HttpContext.Identifier();
        ActorObject.RoleParams roleParams =
            body.MapTo<ActorObject.RoleParams>();

        Role role = await _updateRoleActorHandler.ExecuteAsync(
            identifier,
            roleId,
            roleParams
        );
        return Ok(role);
    }

    ///
    /// Summary:
    ///     Removes a role granted to an owned sub-account.
    ///
    [
        Permission(nameof(PermissionConstant.AUTH_ACTOR)),
        HttpDelete(RouteConstant.ACTOR_ROLE_REMOVE)
    ]
    public async Task<IActionResult> RemoveRole([FromRoute] Guid roleId)
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<Role> roles = await _removeRoleActorHandler.ExecuteAsync(
            identifier,
            roleId
        );
        return Ok(roles);
    }

    ///
    /// Summary:
    ///     Lists the master permission definitions available for actor roles.
    ///
    [
        Permission(nameof(PermissionConstant.AUTH_ACTOR)),
        HttpGet(RouteConstant.ACTOR_ROLE_PERMISSIONS)
    ]
    public async Task<IActionResult> Permissions()
    {
        List<RoleObject.PermissionMaster> permissions =
            await _permissionActor.ExecuteAsync();
        return Ok(permissions);
    }
}
