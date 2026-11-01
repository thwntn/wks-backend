namespace Wks.Repositories;

public class NotificationRepository(DatabaseContext databaseContext)
    : Repository<Notification>(databaseContext),
        INotificationRepository { }
