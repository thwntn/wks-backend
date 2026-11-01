namespace Wks.Interfaces;

///
/// Summary:
///     Interface for the Unit of Work pattern in Entity Framework.
///     Provides transaction management and change persistence operations.
///
public interface IUnitOfWork
{
    ///
    /// Summary:
    ///     Synchronizes all pending changes to the database.
    ///     Commits all tracked entity changes in a single transaction.
    ///
    /// Returns:
    ///     Number of entities affected by the save operation.
    ///
    Task<int> SyncAsync();
}
