namespace Wks.Interfaces;

///
/// Summary:
///     Use case interface for retrieving current session JWT token information.
///     Defines contract for extracting authenticated user data from HTTP context.
///
public interface ISessionTokenInformation
{
    ///
    /// Summary:
    ///     Retrieves and decodes JWT token information from the current HTTP context.
    ///     Reads token from authorization header and extracts payload data.
    ///
    /// Returns:
    ///     UserJsonWebToken object containing decoded JWT payload information.
    ///
    UserJsonWebToken Execute();
}
