namespace Wks.Ports;

public interface IRealtimeTimelinePort
{
    Task ExecuteAsync(Guid profileId);
}
