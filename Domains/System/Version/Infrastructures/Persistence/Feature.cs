namespace Wks.Repositories;

public class FeatureVersionRepository(DatabaseContext _databaseContext)
    : Repository<AppFeature>(_databaseContext),
        IFeatureRepository { }
