namespace Wks.UseCases;

public class GetActivityFinancialAnalytics(IActionRepository _actionRepository)
    : IGetActivitiesFinancialAnalytics
{
    public async Task<List<Databases.Action>> ExecuteAsync(Guid profileId)
    {
        List<Databases.Action> activities =
            await EntityFrameworkQueryableExtensions.ToListAsync(
                _actionRepository.Where(activity =>
                    activity.ProfileId == profileId
                )
            );

        return activities;
    }
}
