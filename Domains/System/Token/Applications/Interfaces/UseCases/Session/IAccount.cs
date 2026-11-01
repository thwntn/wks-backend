namespace Wks.Interfaces;

///
/// Summary:
///     Use case interface for retrieving authenticated user's account information.
///     Defines contract for fetching account data from current session context.
///
public interface IGetAccount
{
    ///
    /// Summary:
    ///     Retrieves the current authenticated user's account information.
    ///     Extracts user data from JWT token and queries database for account details.
    ///
    /// Returns:
    ///     The Account object containing user account information.
    ///
    Account Execute();
}
