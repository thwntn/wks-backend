namespace Wks.Repositories;

public class ProfileRepository(DatabaseContext databaseContext)
    : Repository<Profile>(databaseContext),
        IProfileRepository { }
