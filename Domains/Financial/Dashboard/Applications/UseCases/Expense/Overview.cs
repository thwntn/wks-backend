namespace Wks.UseCases;

public class OverviewDashboard() : IOverviewDashboard
{
    public Task<DashboardObject.Overview> ExecuteAsync(Guid profileId) =>
        Task.FromResult<DashboardObject.Overview>(default);
}
