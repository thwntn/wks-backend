namespace Wks.UseCases;

///
/// Summary:
///     Retrieves Google OAuth access tokens from authorization codes.
///     Exchanges authorization codes for access tokens through Google's OAuth endpoint.
///
public class GetAccessTokenGoogle(IRequest _request) : IGetAccessTokenGoogle
{
    ///
    /// Summary:
    ///     Retrieves an access token from Google OAuth using an authorization code.
    ///     Exchanges the authorization code through Google's OAuth endpoint.
    ///
    /// Parameters:
    ///     authorizationCode:
    ///         The authorization code received from the Google OAuth flow.
    ///
    ///     setting:
    ///         Google OAuth client credentials supplied by the caller.
    ///
    /// Returns:
    ///     An access token response containing the token and metadata.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when Google does not return a successful token response.
    ///
    public async Task<GoogleObject.AccessTokenResponse> ExecuteAsync(
        string authorizationCode,
        GoogleObject.Setting setting
    )
    {
        /// Logic:
        ///     Build the Google OAuth token endpoint URL from runtime environment.
        ///
        string token,
            url = Format.ToPathName(Runtime.GoogleOauthHost.Env(), nameof(token));

        /// Logic:
        ///     Build the token exchange payload from caller credentials and runtime grant type.
        ///
        GoogleObject.AccessTokenPayload accessTokenPayload = new(
            authorizationCode,
            setting.ClientId,
            setting.ClientSecret,
            setting.RedirectUri,
            Runtime.GoogleGrandType.Env()
        );

        HttpObject.Response<GoogleObject.AccessTokenResponse> accessTokenResponse =
            await _request.FormUrlEncoded<GoogleObject.AccessTokenResponse>(
                url,
                new FormUrlEncodedContent(
                    accessTokenPayload.MapTo<Dictionary<string, string>>()
                )
            );

        if (accessTokenResponse.IsSuccessStatusCode is false)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.GOOGLE_NOT_FOUND_ACCESS_TOKEN
            );

        return accessTokenResponse.Data;
    }
}
