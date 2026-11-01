namespace Wks.Interfaces;

public interface IOverviewDashboard
{
    Task<DashboardObject.Overview> ExecuteAsync(Guid profileId);
}
