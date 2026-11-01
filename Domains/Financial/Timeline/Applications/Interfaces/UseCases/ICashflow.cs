namespace Wks.Interfaces;

public interface ICashflowTimeline
{
    Task<List<TimelineObject.Cashflow>> ExecuteAsync(Guid profileId);
}
