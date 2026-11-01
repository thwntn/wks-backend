namespace Wks.UseCases;

///
/// Summary:
///     Use case for signing the subject identifier supplied by token callers.
///
public class GenerateJsonWebToken : IGenerateAccessToken
{
    private readonly string _issuer = Runtime.Issuer.Env();
    private readonly string _jwtExpire = Runtime.JwtExpire.Env();
    private readonly string _audience = Runtime.Audience.Env();
    private readonly string _jwtKey = Runtime.JwtKey.Env();

    ///
    /// Summary:
    ///     Signs a JWT without resolving account type or parent ownership.
    ///
    /// Parameters:
    ///     refreshToken:
    ///         The stored token whose subject identifier is copied without conversion.
    ///
    /// Returns:
    ///     The JWT containing the supplied Identifier claim.
    ///
    /// Logic:
    ///     Copies the refresh-token identifier into the claims dictionary.
    ///     Signs the claims using the configured issuer, audience and key.
    ///
    public Task<string> ExecuteAsync(RefreshToken refreshToken)
    {
        Dictionary<string, object> claims = [];
        claims.Add(
            nameof(UserJsonWebToken.Identifier),
            string.Concat(refreshToken.Identifier)
        );
        return Task.FromResult(SignToken(claims));
    }

    ///
    /// Summary:
    ///     Signs the supplied claims using the configured JWT settings.
    ///
    /// Parameters:
    ///     claims:
    ///         The unchanged subject identifier to embed in the token.
    ///
    /// Returns:
    ///     The serialized JWT signed with HMAC-SHA256.
    ///
    /// Logic:
    ///     Creates the descriptor with configured expiration and signing credentials.
    ///     Signs and serializes the descriptor.
    ///
    private string SignToken(Dictionary<string, object> claims)
    {
        SecurityTokenDescriptor descriptor = new()
        {
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_jwtExpire)),
            Issuer = _issuer,
            Audience = _audience,
            Claims = claims,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                SecurityAlgorithms.HmacSha256
            ),
        };
        JwtSecurityTokenHandler handler = new();
        SecurityToken token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
