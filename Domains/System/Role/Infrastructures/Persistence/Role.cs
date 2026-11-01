namespace Wks.Repositories;

///
/// Summary:
///     Repository implementation for role persistence operations.
///     Provides database access for Role entity CRUD operations.
///
public class RoleRepository(DatabaseContext databaseContext)
    : Repository<Role>(databaseContext),
        IRoleRepository { }
