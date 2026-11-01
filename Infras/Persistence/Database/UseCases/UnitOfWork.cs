namespace Wks.Mains;

///
/// Summary:
///     Implementation of the Unit of Work pattern for Entity Framework.
///     Coordinates changes across multiple repositories and persists them in a single transaction.
///
public class UnitOfWork(DatabaseContext databaseContext) : IUnitOfWork
{
    private readonly DatabaseContext _databaseContext = databaseContext;

    ///
    /// Summary:
    ///     Synchronizes all pending changes to the database.
    ///     Commits all tracked entity changes in a single transaction.
    ///
    /// Returns:
    ///     Number of entities affected by the save operation.
    ///
    public async Task<int> SyncAsync() =>
        await _databaseContext.SaveChangesAsync();
}
