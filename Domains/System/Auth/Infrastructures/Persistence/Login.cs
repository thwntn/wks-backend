namespace Wks.UseCases;

///
/// Summary:
///     Repository implementation for LoginAccount entity data access.
///     Provides database operations for login history tracking.
///
public class LoginAccountRepository(DatabaseContext databaseContext)
    : Repository<LoginAccount>(databaseContext),
        ILoginAccountRepository { }
