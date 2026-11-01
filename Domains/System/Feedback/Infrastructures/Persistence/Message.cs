namespace Wks.Repositories;

public class FeedbackMessageRepository(DatabaseContext databaseContext)
    : Repository<FeedbackMessage>(databaseContext),
        IFeedbackMessageRepository { }
