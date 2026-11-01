namespace Wks.Interfaces;

public interface IUpdateTimeline
{
    Task<Timeline> ExecuteAsync(Guid profileId, TimelineDataTransformer.Update update);
}
