namespace Wks.UseCases;

///
/// Summary:
///     Retrieves the authenticated user's account information from the current session.
///     Extracts user data from JWT token and queries the database for account details.
///
public class GetAccount(
    ISessionTokenInformation _getInformation,
    DatabaseContext _databaseContext
) : IGetAccount
{
    ///
    /// Summary:
    ///     Retrieves the current authenticated user's account information.
    ///     Reads JWT token information and queries database for account entity.
    ///
    /// Returns:
    ///     The Account object containing user account information.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when account is not found in the database with BadRequest status.
    ///
    public Account Execute()
    {
        /// Logic:
        ///     Extract user information from current session JWT token.
        ///
        UserJsonWebToken information = _getInformation.Execute();

        /// Logic:
        ///     Query database for account matching token information.
        ///     Throw HttpException if account is not found.
        ///
        Account account =
            _databaseContext.Account.FirstOrDefault()
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        return account;
    }
}
