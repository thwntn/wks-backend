namespace Wks.Ports;

public interface IRealtimeExpensePort
{
    Task ExecuteAsync(Guid profileId);
}
