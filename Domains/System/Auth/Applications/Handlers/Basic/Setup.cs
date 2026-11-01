namespace Wks.Handlers;

///
/// Summary:
///     Orchestrates account setup operation with transaction management.
///     Manages role assignment and domain configuration for new accounts.
///
public class SetupAccountAuthHandler(
    ISetupAccountAuth _setupAccountAuth,
    IUnitOfWork _unitOfWork
) : ISetupAccountAuthHandler
{
    ///
    /// Summary:
    ///     Executes account setup process including default roles and domain.
    ///     Ensures atomic transaction for all setup operations.
    ///
    /// Parameters:
    ///     accountId:
    ///         The unique identifier of the account to setup.
    ///
    /// Returns:
    ///     Task representing the asynchronous setup operation.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when account or required settings are not found.
    ///
    public async Task ExecuteAsync(Guid accountId)
    {
        /// Logic:
        ///     Execute account setup use case to assign roles and configure domain.
        ///
        await _setupAccountAuth.ExecuteAsync(accountId);

        /// Logic:
        ///     Commit database transaction to persist setup changes.
        ///
        await _unitOfWork.SyncAsync();
    }
}
