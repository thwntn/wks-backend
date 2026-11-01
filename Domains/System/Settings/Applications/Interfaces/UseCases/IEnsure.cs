namespace Wks.Interfaces;

///
/// Summary:
///     Contract for verifying ADMIN_SETTING on the current account.
///     Used by setting mutation handlers before create, update, or remove.
///
public interface IEnsureAdminSetting
{
    ///
    /// Summary:
    ///     Verifies the account mapped from the profile identifier has ADMIN_SETTING.
    ///
    /// Parameters:
    ///     profileId:
    ///         The authenticated profile identifier from the request token.
    ///
    /// Returns:
    ///     Task representing the asynchronous authorization check.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the account is not found or ADMIN_SETTING is not assigned.
    ///
    Task ExecuteAsync(Guid profileId);
}
