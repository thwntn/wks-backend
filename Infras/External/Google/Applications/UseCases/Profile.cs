namespace Wks.UseCases;

///
/// Summary:
///     Retrieves user profile information from Google using OAuth access tokens.
///     Fetches user details from Google's userinfo API endpoint.
///
public class GetProfileGoogle(IRequest _request) : IGetProfileGoogle
{
    ///
    /// Summary:
    ///     Retrieves user profile information from Google using an access token.
    ///     Fetches user details from Google's userinfo API endpoint.
    ///
    /// Parameters:
    ///     accessToken:
    ///         The access token obtained from Google OAuth.
    ///
    /// Returns:
    ///     A profile response containing user information such as identifier, email, and name.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when Google does not return a successful profile response.
    ///
    public async Task<GoogleObject.ProfileResponse> ExecuteAsync(
        string accessToken
    )
    {
        /// Logic:
        ///     Build the Google userinfo endpoint URL from runtime environment.
        ///
        string Bearer,
            v2,
            userinfo,
            url = Format.ToPathName(
                Runtime.GoogleApiHost.Env(),
                nameof(v2),
                nameof(userinfo)
            );

        string authorization = $"{nameof(Bearer)} {accessToken}";
        HttpObject.Response<GoogleObject.ProfileResponse> profileResponse =
            await _request.GetAsync<GoogleObject.ProfileResponse>(
                url,
                default,
                new() { [nameof(authorization)] = authorization }
            );

        if (profileResponse.IsSuccessStatusCode is false)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.GOOGLE_NOT_FOUND_PROFILE
            );

        return profileResponse.Data;
    }
}
