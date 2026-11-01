namespace Wks.Mains;

///
/// Summary:
///     Provides request-time profile resolution independently of JWT generation.
///
public static class TokenIdentityExtensions
{
    ///
    /// Summary:
    ///     Resolves the profile scope used by an authenticated controller action.
    ///
    /// Parameters:
    ///     httpContext:
    ///         The request containing the authenticated account token.
    ///
    ///     personal:
    ///         True for the signed-in user profile; false for the business profile scope.
    ///
    /// Returns:
    ///     The profile ID selected by the identity use case from current account data.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the token, account or required profile cannot be resolved.
    ///
    /// Logic:
    ///     Reads the unchanged account identifier and delegates database resolution
    ///     to the scoped identity use case without changing token claims.
    ///
    public static async Task<Guid> Identifier(
        this HttpContext httpContext,
        bool personal = false
    )
    {
        Guid accountId = Account(httpContext);
        IResolveIdentityToken resolver =
            httpContext.RequestServices.GetRequiredService<IResolveIdentityToken>();
        return await resolver.ExecuteAsync(accountId, personal);
    }

    ///
    /// Summary:
    ///     Reads the original account identifier from the authenticated principal.
    ///
    /// Parameters:
    ///     httpContext:
    ///         The request whose JWT has been validated by authentication middleware.
    ///
    /// Returns:
    ///     The non-empty account ID stored in the Identifier claim.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when authentication or the required identifier claim is invalid.
    ///
    /// Logic:
    ///     Requires an authenticated identity before accessing the subject claim.
    ///     Parses the account ID without decoding another copy of the bearer token.
    ///
    private static Guid Account(HttpContext httpContext)
    {
        if (
            httpContext.User.Identity.Justifiable() is false
            || httpContext.User.Identity.IsAuthenticated == false
        )
            throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.JWT_CAN_NOT_READ_TOKEN
            );

        System.Security.Claims.Claim identifierClaim =
            httpContext.User.FindFirst(nameof(UserJsonWebToken.Identifier));

        if (
            identifierClaim.Justifiable() is false
            || Guid.TryParse(identifierClaim.Value, out Guid accountId) == false
            || accountId == Guid.Empty
        )
            throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.JWT_CAN_NOT_READ_TOKEN
            );

        return accountId;
    }
}
