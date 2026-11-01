namespace Wks.Interfaces;

public interface ICreateTimeline
{
    Timeline Execute(Guid profileId, TimelineDataTransformer.Create create);
}
