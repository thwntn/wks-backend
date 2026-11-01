namespace Wks.Interfaces;

///
/// Summary:
///     Use case interface for parsing JWT tokens from HTTP requests.
///     Defines contract for extracting and decoding JWT payload information.
///
public interface IParseToken
{
    ///
    /// Summary:
    ///     Parses JWT token from HTTP request authorization header.
    ///     Extracts Bearer token, decodes JWT payload, and maps to UserJsonWebToken object.
    ///
    /// Parameters:
    ///     httpRequest:
    ///         The HTTP request containing the authorization header with JWT token.
    ///
    /// Returns:
    ///     UserJsonWebToken object containing decoded JWT payload information.
    ///
    UserJsonWebToken Execute(HttpRequest httpRequest);
}
