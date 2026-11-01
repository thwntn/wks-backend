namespace Wks.Interfaces;

///
/// Summary:
///     Commits permission synchronization and invalidates authorization caches.
///
public interface ISyncFeaturesAdminHandler
{
    ///
    /// Summary:
    ///     Returns the number of synchronized accounts.
    ///
    Task<int> ExecuteAsync();
}
