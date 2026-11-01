namespace Wks.Interfaces;

///
/// Summary:
///     Handler contract for soft-deleting system settings after admin authorization.
///     Commits the unit of work and invalidates the settings cache.
///
public interface IRemoveSettingHandler
{
    ///
    /// Summary:
    ///     Authorizes the caller, soft-deletes settings, then commits and invalidates cache.
    ///
    /// Parameters:
    ///     profileId:
    ///         The authenticated profile identifier used for the admin check and DeletedBy.
    ///
    ///     ids:
    ///         The setting identifiers to soft-delete.
    ///
    /// Returns:
    ///     An empty string after a successful remove.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the caller is not an admin or no matching settings are found.
    ///
    Task<string> ExecuteAsync(Guid profileId, List<Guid> ids);
}
