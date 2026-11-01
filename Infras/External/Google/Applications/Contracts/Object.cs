namespace Wks.Objects;

///
/// Summary:
///     Provides data structures for Google OAuth and profile integration.
///     Includes payload models for authentication and user profile retrieval.
///
public class GoogleObject
{
    ///
    /// Summary:
    ///     Google OAuth client credentials supplied by the caller.
    ///     Hosts and grant type are read from runtime environment.
    ///
    public class Setting(string clientId, string clientSecret, string redirectUri)
    {
        ///
        /// Summary:
        ///     OAuth client identifier issued by Google.
        ///
        /// Returns:
        ///     Client identifier string.
        ///
        public string ClientId { get; set; } = clientId;

        ///
        /// Summary:
        ///     OAuth client secret issued by Google.
        ///
        /// Returns:
        ///     Client secret string.
        ///
        public string ClientSecret { get; set; } = clientSecret;

        ///
        /// Summary:
        ///     Redirect URI registered with the Google OAuth client.
        ///
        /// Returns:
        ///     Redirect URI string.
        ///
        public string RedirectUri { get; set; } = redirectUri;
    }

    ///
    /// Summary:
    ///     Represents the request payload for obtaining a Google OAuth access token.
    ///     Contains credentials and authorization code for token exchange.
    ///
    public class AccessTokenPayload(
        string code,
        string clientId,
        string clientSecret,
        string redirectUri,
        string grantType
    )
    {
        public string client_secret = clientSecret;
        public string redirect_uri = redirectUri;
        public string code = code;
        public string client_id = clientId;
        public string grant_type = grantType;
    }

    ///
    /// Summary:
    ///     Represents the response from Google OAuth token endpoint.
    ///     Contains access token and metadata for API authorization.
    ///
    public class AccessTokenResponse
    {
        public string access_token;
        public string expires_in;
        public string scope;
        public string token_type;
        public string id_token;
    }

    ///
    /// Summary:
    ///     Represents user profile information retrieved from Google API.
    ///     Contains user identity, email, name, and profile picture URL.
    ///
    public class ProfileResponse
    {
        public string id;
        public string email;
        public string verified_email;
        public string name;
        public string given_name;
        public string family_name;
        public string picture;
    }
}
