namespace Wks.Handlers;

///
/// Summary:
///     Handler interface for JSON Web Token creation orchestration.
///     Defines contract for complete JWT and refresh token generation workflow.
///
public interface ICreateJsonWebTokenHandler
{
    ///
    /// Summary:
    ///     Creates or refreshes JWT access token and refresh token pair.
    ///     Orchestrates token validation, generation, and lifecycle management.
    ///
    /// Parameters:
    ///     identifier:
    ///         The unique identifier of the user for whom to generate tokens.
    ///
    /// Returns:
    ///     A task that represents the asynchronous operation containing TokenObject.Generated with token pair.
    ///
    Task<TokenObject.Generated> ExecuteAsync(Guid identifier);
}
