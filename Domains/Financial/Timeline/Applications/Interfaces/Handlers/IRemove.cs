namespace Wks.Interfaces;

public interface IRemoveTimelineHandler
{
    Task<string> ExecuteAsync(Guid identifier, Guid id);
}
