namespace Wks.Interfaces;

public interface IGetActivitiesFinancialAnalytics
{
    Task<List<Databases.Action>> ExecuteAsync(Guid profileId);
}
