namespace Wks.Repositories;

///
/// Summary:
///     Concrete repository implementation for refresh token persistence operations.
///     Provides database access for RefreshToken entity CRUD operations.
///
public class RefreshTokenRepository(DatabaseContext databaseContext)
    : Repository<RefreshToken>(databaseContext),
        IRefreshTokenRepository { }
