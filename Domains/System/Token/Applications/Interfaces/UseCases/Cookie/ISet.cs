namespace Wks.Interfaces;

///
/// Summary:
///     Use case interface for setting up authentication cookies.
///     Defines contract for configuring secure cookies with account data.
///
public interface ISetupCookie
{
    ///
    /// Summary:
    ///     Sets authentication cookie with serialized account data.
    ///     Configures cookie with secure settings including expiration and same-site policy.
    ///
    /// Parameters:
    ///     responseCookies:
    ///         The response cookies collection to append the authentication cookie.
    ///
    ///     data:
    ///         The account data object to serialize and store in the cookie.
    ///
    void Execute(IResponseCookies responseCookies, object data);
}
