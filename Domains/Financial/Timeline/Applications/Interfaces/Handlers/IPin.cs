namespace Wks.Interfaces;

public interface IPinTimelineHandler
{
    Task<Timeline> ExecuteAsync(Guid identifier, Guid id);
}
