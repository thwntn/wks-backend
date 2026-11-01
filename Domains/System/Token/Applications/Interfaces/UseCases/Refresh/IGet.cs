namespace Wks.UseCases;

///
/// Summary:
///     Use case interface for retrieving active refresh tokens by user identifier.
///     Defines contract for querying the most recent valid refresh token.
///
public interface IGetRefreshToken
{
    ///
    /// Summary:
    ///     Retrieves the most recent active refresh token for the specified identifier.
    ///     Queries repository ordered by creation date and filters by identifier and active status.
    ///
    /// Parameters:
    ///     identifier:
    ///         The unique identifier of the user whose refresh token to retrieve.
    ///
    /// Returns:
    ///     A task that represents the asynchronous operation containing the RefreshToken if found, null otherwise.
    ///
    Task<RefreshToken> ExecuteAsync(Guid identifier);
}
