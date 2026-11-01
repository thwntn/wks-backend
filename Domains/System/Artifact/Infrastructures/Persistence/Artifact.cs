namespace Wks.Repositories;

public class ArtifactRepository(DatabaseContext databaseContext)
    : Repository<Artifact>(databaseContext),
        IArtifactRepository { }
