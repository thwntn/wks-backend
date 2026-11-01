namespace Wks.Repositories;

public class PermissionRepository(DatabaseContext databaseContext)
    : Repository<Permission>(databaseContext),
        IPermissionRepository { }
