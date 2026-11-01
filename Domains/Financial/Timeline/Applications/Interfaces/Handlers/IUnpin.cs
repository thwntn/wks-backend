namespace Wks.Interfaces;

public interface IUnpinTimelineHandler
{
    Task<Timeline> ExecuteAsync(Guid identifier, Guid id);
}
