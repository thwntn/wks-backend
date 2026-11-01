namespace Wks.Controllers;

[ApiController, Route(RouteConstant.ROLE), Tags(SwaggerConstant.ROLES)]
public class RoleController(IMakeAdminHandler _makeAdminHandler)
    : ControllerBase
{
    ///
    /// Summary:
    ///     Replaces all target account roles and permissions with a default role granting every permission.
    ///     Access is gated by a shared secret: the raw Authorization header must
    ///     match the AdminRequiredPass environment variable, otherwise returns 403.
    ///
    [AllowAnonymous, HttpPatch(RouteConstant.ROLE_MAKE_ADMIN)]
    public async Task<IActionResult> MakeAdmin(
        [FromBody] RoleDataTransformer.MakeAdmin body
    )
    {
        /// Logic:
        ///     Read the raw Authorization header and compare it against the
        ///     configured admin pass. Reject with 403 when it is missing or
        ///     does not match.
        ///
        string authorization = Request.Headers[HeaderNames.Authorization];
        string adminRequiredPass = Runtime.AdminRequiredPass.Env();

        if (
            adminRequiredPass.Justifiable() is false
            || string.Equals(
                authorization,
                adminRequiredPass,
                StringComparison.Ordinal
            )
                is false
        )
            throw new HttpException(
                HttpStatus.Forbidden,
                MessageConstant.ADMIN_PASS_INVALID
            );

        Role role = await _makeAdminHandler.ExecuteAsync(body.AccountId);
        return Ok(role);
    }
}
