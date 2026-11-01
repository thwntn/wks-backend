namespace Wks.Repositories;

public class IconRepository(DatabaseContext databaseContext)
    : Repository<Icon>(databaseContext),
        IIconRepository { }
