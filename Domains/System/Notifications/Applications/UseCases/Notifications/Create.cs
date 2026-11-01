namespace Wks.UseCases;

public class CreateNotification(DatabaseContext _databaseContext)
    : ICreateNotification
{
    public Task<Notification> ExecuteAsync(
        Guid profileId,
        Guid fromAccount,
        NotificationNames type,
        object jsonData
    )
    {
        Notification notification = new()
        {
            JsonData = jsonData.Serialize(),
            Type = type,
            FromId = fromAccount,
            IsRead = false,
            Handle = false,
            ProfileId = profileId,
        };

        _databaseContext.Add(notification);

        return Task.FromResult(notification);
    }
}
