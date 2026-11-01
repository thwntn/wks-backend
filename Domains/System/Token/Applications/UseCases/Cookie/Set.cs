namespace Wks.UseCases;

///
/// Summary:
///     Sets up authentication cookies for client-side session management.
///     Configures secure cookie settings with account data serialization.
///
public class SetupCookie : ISetupCookie
{
    ///
    /// Summary:
    ///     The session timeout duration for authentication cookies.
    ///
    /// Returns:
    ///     DateTime representing cookie expiration set to 14 days from creation.
    ///
    private readonly DateTime _timeoutSession = DateTime.UtcNow.AddDays(14);

    ///
    /// Summary:
    ///     Sets authentication cookie with serialized account data.
    ///     Configures cookie with strict same-site policy and 14-day expiration.
    ///
    /// Parameters:
    ///     responseCookies:
    ///         The response cookies collection to append the authentication cookie.
    ///
    ///     data:
    ///         The account data object to serialize and store in the cookie.
    ///
    public void Execute(IResponseCookies responseCookies, object data)
    {
        /// Logic:
        ///     Append authentication cookie with serialized account data.
        ///     Configure cookie with strict same-site mode, essential flag, non-secure for development,
        ///     14-day expiration, and non-HTTP-only for client-side access.
        ///
        responseCookies.Append(
            nameof(Cookie),
            data.MapTo<Account>().Serialize(),
            new()
            {
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                IsEssential = true,
                Secure = false,
                Domain = string.Empty,
                Expires = _timeoutSession,
                HttpOnly = false,
            }
        );
    }
}
