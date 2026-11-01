namespace Wks.Objects;

///
/// Summary:
///     Data transfer object representing decoded JWT token payload information.
///     Contains user authentication claims and standard JWT fields.
///
public class UserJsonWebToken
{
    ///
    /// Summary:
    ///     The unique identifier of the authenticated user.
    ///
    /// Returns:
    ///     GUID value representing the user's unique identifier.
    ///
    [NotMapped]
    public Guid Identifier { get; set; }

    ///
    /// Summary:
    ///     The unique identifier of the acting actor account when the token is issued
    ///     for an actor session.
    ///
    /// Returns:
    ///     GUID value representing the actor account identifier, empty when the token
    ///     is issued for a basic account.
    ///
    [NotMapped]
    public Guid Actor { get; set; }

    ///
    /// Summary:
    ///     The unique identifier of the parent account for hierarchical organization.
    ///
    /// Returns:
    ///     GUID value representing the parent account identifier, empty if no parent exists.
    ///
    [NotMapped]
    public Guid ParentId { get; set; }

    ///
    /// Summary:
    ///     The role assigned to the authenticated user for authorization.
    ///
    /// Returns:
    ///     String value representing the user's role.
    ///
    [NotMapped]
    public string Role { get; set; }

    ///
    /// Summary:
    ///     The "not before" timestamp indicating when the token becomes valid.
    ///
    /// Returns:
    ///     Integer Unix timestamp representing the token's validity start time.
    ///
    [NotMapped]
    public int Nbf { get; set; }

    ///
    /// Summary:
    ///     The expiration timestamp indicating when the token becomes invalid.
    ///
    /// Returns:
    ///     Integer Unix timestamp representing the token's expiration time.
    ///
    [NotMapped]
    public int Exp { get; set; }

    ///
    /// Summary:
    ///     The "issued at" timestamp indicating when the token was created.
    ///
    /// Returns:
    ///     Integer Unix timestamp representing the token's creation time.
    ///
    [NotMapped]
    public int Iat { get; set; }

    ///
    /// Summary:
    ///     The issuer claim identifying the token creator.
    ///
    /// Returns:
    ///     String value representing the token issuer.
    ///
    [NotMapped]
    public string Iss { get; set; }

    ///
    /// Summary:
    ///     The audience claim identifying the intended token recipient.
    ///
    /// Returns:
    ///     String value representing the token audience.
    ///
    [NotMapped]
    public string Aud { get; set; }
}

///
/// Summary:
///     Container class for token-related data transfer objects.
///     Provides structured response objects for token generation operations.
///
public class TokenObject
{
    ///
    /// Summary:
    ///     Data transfer object containing generated token pair.
    ///     Used for returning both access and refresh tokens in authentication responses.
    ///
    public class Generated(string accessToken, string refreshToken)
    {
        ///
        /// Summary:
        ///     The generated JWT access token for API authentication.
        ///
        /// Returns:
        ///     String containing the signed JWT access token.
        ///
        public string AccessToken { get; set; } = accessToken;

        ///
        /// Summary:
        ///     The generated refresh token for obtaining new access tokens.
        ///
        /// Returns:
        ///     String containing the cryptographically secure refresh token.
        ///
        public string RefreshToken { get; set; } = refreshToken;
    }
}
