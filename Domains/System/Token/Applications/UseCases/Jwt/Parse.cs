namespace Wks.UseCases;

///
/// Summary:
///     Parses and extracts JWT token information from HTTP request authorization headers.
///     Decodes JWT payload and maps it to user information structure.
///
public class ParseToken : Interfaces.IParseToken
{
    ///
    /// Summary:
    ///     Parses JWT token from HTTP request authorization header and extracts user information.
    ///     Reads Bearer token, decodes JWT payload, and maps to UserJsonWebToken object.
    ///
    /// Parameters:
    ///     httpRequest:
    ///         The HTTP request containing the authorization header with JWT token.
    ///
    /// Returns:
    ///     UserJsonWebToken object containing decoded JWT payload information.
    ///
    public UserJsonWebToken Execute(HttpRequest httpRequest)
    {
        /// Logic:
        ///     Create JWT security token handler for token processing.
        ///
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

        /// Logic:
        ///     Parse authorization header and extract Bearer token parameter.
        ///
        _ = AuthenticationHeaderValue.TryParse(
            httpRequest.Headers[HeaderNames.Authorization],
            out AuthenticationHeaderValue headerValue
        );

        /// Logic:
        ///     Read and decode JWT token from authorization header parameter.
        ///
        SecurityToken token = jwtSecurityTokenHandler.ReadToken(
            headerValue.Parameter
        );

        /// Logic:
        ///     Cast generic security token to JWT security token and map payload to UserJsonWebToken.
        ///
        JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)token;
        return jwtSecurityToken.Payload.MapTo<UserJsonWebToken>();
    }
}
