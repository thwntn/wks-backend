namespace Wks.Mains;

///
/// Summary:
///     Contains constants for HTTP header names used in the application.
///
public class HeaderNameConstant
{
    ///
    /// Summary:
    ///     Cloudflare connecting IP address header name.
    ///
    public const string CfConnectingIp = "cf-connecting-ip";

    ///
    /// Summary:
    ///     Real IP address header name for proxy configurations.
    ///
    public const string XRealIp = "x-real-ip";

    ///
    /// Summary:
    ///     Forwarded for header name containing original client IP.
    ///
    public const string XForwardedFor = "x-forwarded-for";

    ///
    /// Summary:
    ///     Webhook secret header name for webhook authentication.
    ///
    public const string XWebhookSecret = "x-webhook-secret";
}

///
/// Summary:
///     Provides helper methods for parsing HTTP request headers.
///
public static class HeaderHelper
{
    ///
    /// Summary:
    ///     Parses and extracts IP address from HTTP request headers.
    ///     Checks multiple headers in priority order for IP resolution.
    ///
    /// Parameters:
    ///     httpRequest:
    ///         The HTTP request to extract IP address from.
    ///
    /// Returns:
    ///     A comma-separated string of IP addresses found in headers.
    ///
    public static string ParseIP(HttpRequest httpRequest)
    {
        List<string> ipAddress = [];

        /// Logic:
        ///     Extract IP addresses from various headers.
        ///     Check Cloudflare connecting IP, X-Real-IP, X-Forwarded-For headers.
        ///     Add remote connection IP if available.
        ///     Return comma-separated list of all found IPs.
        ///
        string cfConnectingIp = httpRequest.Headers[
            HeaderNameConstant.CfConnectingIp
        ];
        string xForwardedFor = httpRequest.Headers[
            HeaderNameConstant.XForwardedFor
        ];
        string xRealIp = httpRequest.Headers[HeaderNameConstant.XRealIp];

        cfConnectingIp.Justifiable(ipAddress.Add);
        xRealIp.Justifiable(ipAddress.Add);
        xForwardedFor.Justifiable(ipAddress.Add);

        IPAddress remoteIpAddress = httpRequest
            .HttpContext
            .Connection
            .RemoteIpAddress;
        remoteIpAddress.Justifiable(remoteIpAddress =>
            ipAddress.Add(remoteIpAddress.ToString())
        );

        return string.Join(QueryOptions.PREFIX_SPLIT, ipAddress);
    }
}
