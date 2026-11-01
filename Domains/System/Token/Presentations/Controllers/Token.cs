namespace Wks.Controllers;

///
/// Summary:
///     API controller for access-token renewal using refresh-token credentials.
///
[ApiController, Route(RouteConstant.TOKEN), Tags(SwaggerConstant.TOKEN)]
public class TokenController(
    IGenerateSessionTokenHandler _generateSessionTokenHandler
) : ControllerBase
{
    ///
    /// Summary:
    ///     Generates a new access token from the refresh token in the route.
    ///
    /// Parameters:
    ///     refreshToken:
    ///         The opaque refresh-token secret supplied by the client.
    ///
    /// Returns:
    ///     HTTP 200 containing the signed access-token string expected by the client.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the refresh token or its subject is invalid.
    ///
    /// Logic:
    ///     Binds the route credential and delegates validation and generation.
    ///     Allows an expired access token because the refresh token authenticates renewal.
    ///
    [AllowAnonymous, HttpGet(RouteConstant.TOKEN_GENERATE)]
    public async Task<IActionResult> Generate([FromRoute] string refreshToken)
    {
        string tokenResult = await _generateSessionTokenHandler.ExecuteAsync(
            refreshToken
        );
        return Ok(tokenResult);
    }
}
