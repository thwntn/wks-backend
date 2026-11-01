namespace Wks.Mains;

///
/// Summary:
///     Provides application-wide exception handling middleware configuration.
///     Maps all unhandled exceptions to a consistent JSON error response.
///
public class Error
{
    ///
    /// Summary:
    ///     Configures the global exception handler for the web application.
    ///     Returns a standardized error payload for any unhandled exception.
    ///
    /// Parameters:
    ///   webApplication:
    ///     The WebApplication instance to configure exception handling on.
    ///
    public static void Configure(WebApplication webApplication) =>
        webApplication.UseExceptionHandler(
            (config) =>
                config.Run(async context =>
                {
                    context.Response.StatusCode = (int)
                        HttpStatus.InternalServerError;
                    await context.Response.WriteAsJsonAsync(
                        new ExceptionResponse(
                            ErrorCodeConstant.SYSTEM_INTERNAL_ERROR,
                            MessageConstant.SYSTEM_INTERNAL_ERROR,
                            context.TraceIdentifier
                        )
                    );
                })
        );
}
