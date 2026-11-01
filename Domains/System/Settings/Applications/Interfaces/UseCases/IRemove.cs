namespace Wks.Interfaces;

///
/// Summary:
///     Contract for soft-deleting system-wide configuration settings.
///     Matching rows stay in the database with deletion metadata.
///
public interface IRemoveSetting
{
    ///
    /// Summary:
    ///     Marks the requested active settings as deleted by the current profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The authenticated profile identifier recorded as DeletedBy.
    ///
    ///     ids:
    ///         The setting identifiers to soft-delete.
    ///
    /// Returns:
    ///     Task representing the asynchronous remove operation.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when none of the requested settings are found or still active.
    ///
    Task ExecuteAsync(Guid profileId, List<Guid> ids);
}
