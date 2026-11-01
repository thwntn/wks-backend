namespace Wks.UseCases;

///
/// Summary:
///     Retrieves current session JWT token information from HTTP context.
///     Extracts and decodes JWT token from the current request's authorization header.
///
public class SessionTokenInformation(IHttpContextAccessor _httpContextAccessor)
    : ISessionTokenInformation
{
    ///
    /// Summary:
    ///     Retrieves and decodes JWT token information from the current HTTP context.
    ///     Reads token from authorization header and extracts payload data.
    ///
    /// Returns:
    ///     UserJsonWebToken object containing decoded JWT payload information.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when JWT token cannot be read or parsed with Unauthorized status.
    ///
    public UserJsonWebToken Execute()
    {
        try
        {
            /// Logic:
            ///     Create JWT security token handler for token processing.
            ///
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            /// Logic:
            ///     Parse authorization header from current HTTP context and extract Bearer token parameter.
            ///
            _ = AuthenticationHeaderValue.TryParse(
                _httpContextAccessor.HttpContext.Request.Headers[
                    HeaderNames.Authorization
                ],
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
        catch
        {
            /// Logic:
            ///     Throw HttpException with Unauthorized status when token reading fails.
            ///
            throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.JWT_CAN_NOT_READ_TOKEN
            );
        }
    }
}
