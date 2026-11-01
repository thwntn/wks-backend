namespace Wks.Mains;

///
/// Summary:
///     Configures Cross-Origin Resource Sharing (CORS) policy for API access.
///     Feature: (CORS middleware with origin, headers, methods, and credentials configuration)
///
/// -> Enables secure cross-origin requests from specified frontend domains
///     while allowing all HTTP methods, headers, and credential passing.
///
public static class CorsExtension
{
    ///
    /// Summary:
    ///     Registers CORS policy with allowed origins, headers, methods, and credentials.
    ///     Feature: (Policy-based CORS configuration from environment variables)
    ///
    /// -> Allows frontend applications to make API calls from different domains
    ///     with full header and credential support for authenticated requests.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register CORS services.
    ///
    public static void CorsConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Add CORS policy with multiple allowed origins
        ///     Feature: (Origin whitelist from comma-separated environment variable)
        ///
        /// -> Parses allowed origins from configuration and enables
        ///     any headers, methods, and credentials for cross-origin requests.
        ///
        services.AddCors(setup =>
        {
            setup.AddPolicy(
                nameof(PolicyNames.Cors),
                policy => policy
                    .WithOrigins(Runtime.Origins.Env().Split(QueryOptions.PREFIX_SPLIT))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            );

            /// Summary:
            ///     Allows public webhook requests from any website origin.
            ///     Credentialed browser requests are not enabled for this policy.
            setup.AddPolicy(
                nameof(PolicyNames.AllowAll),
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
        });
    }
}
