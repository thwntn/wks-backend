namespace Wks.Controllers;

///
/// Summary:
///     Provides system health monitoring endpoint for service availability checks.
///     Supports uptime monitoring and health status verification.
///
[ApiController, Route(RouteConstant.HEALTH), Tags(SwaggerConstant.HEALTHS)]
public class HealthController() : ControllerBase
{
    ///
    /// Summary:
    ///     Basic health check endpoint that responds with "Pong" status.
    ///     Used by monitoring tools to verify service availability.
    ///
    /// Returns:
    ///     A "Pong" response indicating the service is operational.
    ///
    [HttpGet]
    public IActionResult Ping()
    {
        int Pong;
        return Ok(nameof(Pong));
    }
}
