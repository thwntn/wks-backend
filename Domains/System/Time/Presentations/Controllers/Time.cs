namespace Wks.Controllers;

///
/// Summary:
///     Provides time and timezone information endpoints.
///     Supports retrieving available timezone data and current UTC time.
///
[ApiController, Route(RouteConstant.TIME), Tags(SwaggerConstant.TIME)]
public class TimeController : ControllerBase
{
    ///
    /// Summary:
    ///     Retrieves a list of all available timezone information.
    ///     Returns timezone identifiers and metadata for client timezone handling.
    ///
    /// Returns:
    ///     A list of TimeZoneInfo objects containing timezone data.
    ///
    [HttpGet(RouteConstant.TIME_TIMEZONE)]
    public IActionResult TimeZoneInfo()
    {
        List<TimeZoneInfo> timeZoneInfos = Moment.ListTimeZoneId();
        return Ok(timeZoneInfos);
    }

    ///
    /// Summary:
    ///     Retrieves the current UTC time from the server.
    ///     Provides consistent time reference for client synchronization.
    ///
    /// Returns:
    ///     Current DateTime in UTC format.
    ///
    [HttpGet(RouteConstant.TIME_NOW)]
    public IActionResult Now()
    {
        DateTime timeNow = Moment.Utc.Now;
        return Ok(timeNow);
    }
}
