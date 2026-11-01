namespace Wks.Interfaces;

public interface IChartTimeline
{
    Task<List<TimelineObject.Chart>> ExecuteAsync(Guid profileId);
}
