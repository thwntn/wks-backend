namespace Wks.Controllers;

[ApiController, Route(RouteConstant.DOMAIN), Tags(SwaggerConstant.DOMAIN)]
public class DomainController(IListDomain _listDomain, IGetDomain _getDomain)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        Guid identifier = await HttpContext.Identifier();
        List<Domain> domains = await _listDomain.ExecuteAsync(identifier);
        return Ok(domains);
    }

    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ID)
    ]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Domain domain = await _getDomain.ExecuteAsync(identifier, id);
        return Ok(domain);
    }
}
