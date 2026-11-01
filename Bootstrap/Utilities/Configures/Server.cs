namespace Wks.Mains;

///
/// Summary:
///     Configures Kestrel web server and form options for large file handling.
///     Feature: (Connection limits, request body size, and multipart form configuration)
///
/// -> Applies MaxRequestSize from the environment to request and multipart limits
///     so uploads stay within the configured ceiling.
///
public static class ServerExtension
{
    ///
    /// Summary:
    ///     Maximum HTTP request body size in bytes from MaxRequestSize.
    ///
    /// Returns:
    ///     The configured request size limit applied to Kestrel and multipart forms.
    ///
    private static readonly long MAX_SIZE = long.Parse(
        Runtime.MaxRequestSize.Env()
    );

    ///
    /// Summary:
    ///     Configures Kestrel server limits and form options for large requests.
    ///     Feature: (Concurrent connections, request body size, and multipart body limits)
    ///
    /// -> Keeps connection count unrestricted and caps request size by MaxRequestSize.
    ///
    /// Parameters:
    ///   builder:
    ///     The WebApplicationBuilder instance used to configure web host.
    ///
    public static void ServerConfigure(this WebApplicationBuilder builder)
    {
        ///
        /// Summary:
        ///     Register HttpClientFactory services for dependency injection.
        ///     Feature: (HttpClient creation, connection pooling, and lifetime management)
        ///
        /// -> Enables efficient HTTP client usage across the application
        ///     by reusing underlying connections and preventing socket exhaustion.
        ///
        builder.Services.AddHttpClient();

        ///
        /// Summary:
        ///     Configure form options for multipart body length
        ///     Feature: (FormOptions with MaxRequestSize)
        ///
        /// -> Caps multipart uploads at the MaxRequestSize environment value.
        ///
        builder.Services.Configure<FormOptions>(options =>
            options.MultipartBodyLengthLimit = MAX_SIZE
        );

        ///
        /// Summary:
        ///     Configure Kestrel server connection and request limits
        ///     Feature: (MaxConcurrentConnections and MaxRequestBodySize)
        ///
        /// -> Allows maximum concurrent connections and caps request body
        ///     size using MaxRequestSize from the environment.
        ///
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Limits.MaxConcurrentConnections = int.MaxValue;
            options.Limits.MaxConcurrentUpgradedConnections = int.MaxValue;
            options.Limits.MaxRequestBodySize = MAX_SIZE;
        });
    }
}
