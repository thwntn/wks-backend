namespace Wks.Repositories;

public class ActionRepository(DatabaseContext databaseContext)
    : Repository<Databases.Action>(databaseContext),
        IActionRepository { }
