namespace Wks.Interfaces;

public interface IUpdateTimelineHandler
{
    Task<Timeline> ExecuteAsync(Guid identifier, TimelineDataTransformer.Update update);
}
