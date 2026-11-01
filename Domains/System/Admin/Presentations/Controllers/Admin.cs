namespace Wks.Controllers;

///
/// Summary:
///     API controller for administrator-only system maintenance.
///
[ApiController, Route(RouteConstant.ADMIN), Tags(SwaggerConstant.ADMIN)]
[Permission(nameof(PermissionConstant.ADMIN_SETTING))]
public class AdminController(
    ISyncFeaturesAdminHandler _syncFeaturesAdminHandler,
    IClearCacheAdmin _clearCacheAdmin,
    ISetupDomainAdminHandler _setupDomainAdminHandler,
    IListDomain _listDomain
) : ControllerBase
{
    ///
    /// Summary:
    ///     Synchronizes non-admin users with DEFAULT_PERMISSION.
    ///
    /// Returns:
    ///     The number of updated accounts.
    ///
    /// Logic:
    ///     Invokes _syncFeaturesAdminHandler.ExecuteAsync.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPost(RouteConstant.ADMIN_SYNC_FEATURES)
    ]
    public async Task<IActionResult> SyncFeatures()
    {
        int count = await _syncFeaturesAdminHandler.ExecuteAsync();
        return Ok(count);
    }

    ///
    /// Summary:
    ///     Clears all CacheNames namespaces and their descendant entries.
    ///
    /// Returns:
    ///     An empty success response once cache clearing completes.
    ///
    /// Logic:
    ///     Invokes _clearCacheAdmin.ExecuteAsync.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpDelete(RouteConstant.ADMIN_CACHE)
    ]
    public async Task<IActionResult> ClearCache()
    {
        await _clearCacheAdmin.ExecuteAsync();
        return Ok();
    }

    ///
    /// Summary:
    ///     Registers a domain for an account selected by an administrator.
    ///
    /// Parameters:
    ///     accountId:
    ///         The destination account identifier.
    ///
    ///     create:
    ///         The domain host and routing type.
    ///
    /// Returns:
    ///     The persisted domain belonging to the selected account profile.
    ///
    /// Exceptions:
    ///     HttpException when the destination profile or domain input is invalid.
    ///
    /// Logic:
    ///     Maps the allowed request fields to the action contract.
    ///     Invokes _setupDomainAdminHandler.ExecuteAsync.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPost(RouteConstant.ADMIN_DOMAIN_SETUP)
    ]
    public async Task<IActionResult> SetupDomain(
        [FromRoute] Guid accountId,
        [FromBody] DomainDataTransformer.Create create
    )
    {
        Domain domain = await _setupDomainAdminHandler.ExecuteAsync(
            accountId,
            create.MapTo<DomainAdminObject.SetupParams>()
        );
        return Ok(domain);
    }

    ///
    /// Summary:
    ///     Lists domains for a profile selected by an administrator.
    ///
    /// Parameters:
    ///     profileId:
    ///         The selected profile identifier.
    ///
    /// Returns:
    ///     The non-deleted domains owned by that profile.
    ///
    /// Logic:
    ///     Invokes _listDomain.ExecuteAsync.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_PROFILE_DOMAINS)
    ]
    public async Task<IActionResult> ProfileDomains([FromRoute] Guid profileId)
    {
        List<Domain> items = await _listDomain.ExecuteAsync(profileId);
        return Ok(items);
    }
}
