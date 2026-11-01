namespace Wks.Repositories;

///
/// Summary:
///     Repository implementation for Account entity data access.
///     Provides database operations for user account management.
///
public class AccountRepository(DatabaseContext databaseContext)
    : Repository<Account>(databaseContext),
        IAccountRepository { }
