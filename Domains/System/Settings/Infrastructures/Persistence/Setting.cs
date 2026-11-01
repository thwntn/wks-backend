namespace Wks.Repositories;

///
/// Summary:
///     Provides database access for Setting entity CRUD operations.
///     Manages user and system configuration persistence.
///
public class SettingRepository(DatabaseContext databaseContext)
    : Repository<Setting>(databaseContext),
        ISettingRepository { }
