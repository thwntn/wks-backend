namespace Wks.Repositories;

public class TimelineRepository(DatabaseContext databaseContext)
    : Repository<Timeline>(databaseContext),
        ITimelineRepository { }
