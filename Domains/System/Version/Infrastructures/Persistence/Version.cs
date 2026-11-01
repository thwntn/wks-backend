namespace Wks.Repositories;

public class VersionRepository(DatabaseContext _databaseContext)
    : Repository<AppVersion>(_databaseContext),
        IVersionRepository { }
