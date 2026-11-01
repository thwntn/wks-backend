namespace Wks.Mains;

///
/// Summary:
///     Provides HTTP context extension methods for user authentication and request header access.
///     Extracts user information from JWT tokens and retrieves custom headers.
///
public static class HttpContextExtensions
{
    ///
    /// Summary:
    ///     Extracts and decodes user information from the JWT token in the Authorization header.
    ///     Parses the JWT payload and maps it to UserJsonWebToken object.
    ///
    /// Parameters:
    ///   httpContext:
    ///     The HTTP context containing request headers.
    ///
    /// Returns:
    ///     UserJsonWebToken object containing decoded user claims.
    ///
    /// Exceptions:
    ///   T:eComimac.HttpException:
    ///     Thrown when the JWT token cannot be read or is invalid.
    ///
    public static UserJsonWebToken User(this HttpContext httpContext)
    {
        try
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            _ = AuthenticationHeaderValue.TryParse(
                httpContext.Request.Headers[HeaderNames.Authorization],
                out AuthenticationHeaderValue headerValue
            );

            SecurityToken token = jwtSecurityTokenHandler.ReadToken(headerValue.Parameter);
            JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)token;

            return jwtSecurityToken.Payload.MapTo<UserJsonWebToken>();
        }
        catch
        {
            throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.JWT_CAN_NOT_READ_TOKEN
            );
        }
    }

    ///
    /// Summary:
    ///     Retrieves the "From" header value from the HTTP request.
    ///     Used to identify the origin or source of the request.
    ///
    /// Parameters:
    ///   httpContext:
    ///     The HTTP context containing request headers.
    ///
    /// Returns:
    ///     The value of the "From" header.
    ///
    public static string From(this HttpContext httpContext) =>
        httpContext.Request.Headers[HeaderNames.From];
}
