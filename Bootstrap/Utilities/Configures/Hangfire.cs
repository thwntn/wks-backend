namespace Wks.Mains;

///
/// Summary:
///     Adds Hangfire integration services to the dependency injection container when enabled.
///     Supports Redis storage and job server registration for background processing.
///
public static class HangfireExtension
{
    private static readonly bool IsActive = Runtime.WithJob.Env().SafeBool();

    ///
    /// Summary:
    ///     Adds Hangfire services and server support to the application service collection.
    ///     Configures Hangfire to use Redis storage and console logging when enabled.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection to register Hangfire services on.
    ///
    public static void AddHangfire(this IServiceCollection services)
    {
        /// Summary:
        ///     If the application is not running in development mode or consumer services are not enabled, do not add Hangfire services.
        ///     This allows for a simpler routing structure when consumer-based features are not needed, while still supporting attribute-based routing for all controllers.
        if (IsActive is false)
            return;

        /// Summary:
        ///     Add Hangfire services to the service collection.
        ///     This includes configuring Hangfire to use Redis as the storage provider and specifying the prefix for Redis keys.
        services.AddHangfire(configure =>
            configure
                .UseRedisStorage(
                    StackExchange.Redis.ConnectionMultiplexer.Connect(
                        Runtime.Redis.Env()
                    ),
                    new RedisStorageOptions
                    {
                        Prefix = Runtime.HangfirePrefix.Env(),
                    }
                )
                .UseFilter(new HangfireJobLoggingFilter())
                .UseConsole()
        );

        /// Summary:
        ///     Add Hangfire server services to the service collection.
        ///         This includes configuring Hangfire to use Redis as the storage provider and specifying the prefix for Redis keys.
        services.AddHangfireServer();
    }

    ///
    /// Summary:
    ///     Configures the Hangfire dashboard middleware on the application pipeline.
    ///     Makes the Hangfire dashboard available at the configured job path.
    ///
    /// Parameters:
    ///   app:
    ///     The WebApplication instance to configure Hangfire middleware on.
    ///
    public static void UseHangfire(this WebApplication app)
    {
        /// Logic:
        ///     Skip mounting the dashboard when Hangfire is disabled to avoid
        ///     exposing an endpoint backed by no job server.
        ///
        if (IsActive is false)
            return;

        /// Logic:
        ///     Protect the dashboard with header authentication so only the
        ///     configured operator can inspect and control background jobs.
        ///
        app.UseHangfireDashboard(
            Runtime.PrefixJob.Env(),
            new DashboardOptions
            {
                Authorization = [new HangfireAuthorizationFilter()],
            }
        );
    }
}

///
/// Summary:
///     Dashboard authorization filter that gates the Hangfire UI behind HTTP
///     authentication. The scheme and credentials are read from the HangfireScheme,
///     HangfireUsername, and HangfirePassword environment variables.
///
public sealed class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    ///
    /// Summary:
    ///     Single space that separates the scheme from the credential in the header.
    ///
    private const string SEPARATOR = " ";

    ///
    /// Summary:
    ///     Separator between the username and password in the decoded credential.
    ///
    private const char CREDENTIAL_SEPARATOR = ':';

    ///
    /// Summary:
    ///     Expected number of parts after splitting the decoded credential.
    ///
    private const int CREDENTIAL_PARTS = 2;

    ///
    /// Summary:
    ///     Authorizes the incoming dashboard request by validating the Authorization
    ///     header against the configured scheme and credentials.
    ///
    /// Parameters:
    ///   context:
    ///     The dashboard context carrying the current HTTP request.
    ///
    /// Returns:
    ///     True when the credentials match; otherwise false and a 401 challenge.
    ///
    public bool Authorize(DashboardContext context)
    {
        HttpContext httpContext = context.GetHttpContext();

        string username = Runtime.HangfireUsername.Env();
        string password = Runtime.HangfirePassword.Env();
        string scheme = Runtime.HangfireScheme.Env();
        string schemePrefix = scheme + SEPARATOR;

        /// Logic:
        ///     Read the Authorization header and require the configured scheme.
        ///     Missing or mismatched headers are challenged for credentials.
        ///
        string header =
            httpContext.Request.Headers.Authorization.FirstOrDefault();

        if (
            header.Justifiable() is false
            || header.StartsWith(schemePrefix) is false
        )
            return Challenge(httpContext, scheme);

        /// Logic:
        ///     Decode the Base64 "username:password" payload and split into parts.
        ///
        string encoded = header[schemePrefix.Length..];
        string credential = Encoding.UTF8.GetString(
            Convert.FromBase64String(encoded)
        );
        string[] parts = credential.Split(
            CREDENTIAL_SEPARATOR,
            CREDENTIAL_PARTS
        );

        /// Logic:
        ///     Grant access only when the username (first part) and password
        ///     (last part) both match the configured credentials.
        ///
        if (
            string.Equals(parts.First(), username)
            && string.Equals(parts.Last(), password)
        )
            return true;

        return Challenge(httpContext, scheme);
    }

    ///
    /// Summary:
    ///     Emits an HTTP 401 response with the scheme's authentication challenge so
    ///     the browser prompts the operator for credentials.
    ///
    /// Parameters:
    ///   context:
    ///     The current HTTP context to write the challenge onto.
    ///
    ///   scheme:
    ///     The authentication scheme advertised on the challenge header.
    ///
    /// Returns:
    ///     Always false to deny the current dashboard request.
    ///
    private static bool Challenge(HttpContext context, string scheme)
    {
        context.Response.Headers.WWWAuthenticate = scheme;
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return false;
    }
}
