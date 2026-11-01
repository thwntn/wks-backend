namespace Wks.UseCases;

public class ListNotification(
    DatabaseContext _databaseContext,
    IProfileRepository _profileRepository
) : IListNotification
{
    public async Task<Paged<Notification>> ExecuteAsync(
        Guid profileId,
        NotificationDataTransformers.QueryOptions queryOptions
    )
    {
        IQueryable<Notification> notifications = _databaseContext
            .Notification.Include(notification => notification.Profile)
            .Where(notification => notification.ProfileId == profileId);

        /// Summary:
        ///     Filter by notification types when the caller supplies the Types array.
        ///     A null array skips the filter; an empty array matches no records.
        if (queryOptions.Types.Justifiable())
            notifications = notifications.Where(notification =>
                queryOptions.Types.Contains(notification.Type)
            );

        Paged<Notification> paged = await Pagination.CreateAsync(
            notifications
                .OrderByDescending(item => item.CreatedAt)
                .ThenByDescending(item => item.IsRead),
            queryOptions
        );

        await MapAccount(paged);
        return paged;
    }

    private async Task MapAccount(Paged<Notification> paged)
    {
        /// Summary:
        ///     Get profile ids and merge FromId and ProfileId
        List<Guid> profileIds = paged
            .Data.Select(notification => notification.FromId)
            .Union(paged.Data.Select(notification => notification.ProfileId))
            .ToList();

        /// Summary:
        ///     Setup profile to notification
        ///     Check if profile is from or to profile to From, profile to Profile
        foreach (
            Profile profile in await _profileRepository
                .Where(profile => profileIds.Contains(profile.Id))
                .ToListAsync()
        )
        {
            Notification notification = paged.Data.FirstOrDefault(noti =>
                noti.FromId == profile.Id || noti.ProfileId == profile.Id
            );
            if (notification.Justifiable() is false)
                continue;

            /// Summary:
            ///     Setup profile to notification
            ///     Check if profile is from or to profile to From, profile to Profile
            if (notification.FromId == profile.Id)
                notification.From = profile;
            if (notification.ProfileId == profile.Id)
                notification.Profile = profile;
        }
    }
}
