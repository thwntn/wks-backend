namespace Wks.Repositories;

public class TagRepository(DatabaseContext databaseContext)
    : Repository<Hashtag>(databaseContext),
        IHashTagRepository { }
