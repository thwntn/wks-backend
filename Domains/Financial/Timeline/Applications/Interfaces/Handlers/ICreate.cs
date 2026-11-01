namespace Wks.Interfaces;

public interface ICreateTimelineHandler
{
    Task<Timeline> ExecuteAsync(Guid identifier, TimelineDataTransformer.Create create);
}
