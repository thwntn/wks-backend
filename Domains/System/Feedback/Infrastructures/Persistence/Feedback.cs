namespace Wks.Repositories;

public class FeedbackRepository(DatabaseContext databaseContext)
    : Repository<Feedback>(databaseContext),
        IFeedbackRepository { }
