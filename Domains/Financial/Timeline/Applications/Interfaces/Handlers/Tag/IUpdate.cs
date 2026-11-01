namespace Wks.Interfaces;

public interface IUpdateTagTimelineHandler
{
    Task<IEnumerable<TimelineTag>> ExecuteAsync(
        Guid identifier,
        Guid id,
        TimelineDataTransformer.UpdateTag updateTag
    );
}
