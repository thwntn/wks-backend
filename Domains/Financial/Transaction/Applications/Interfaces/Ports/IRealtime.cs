namespace Wks.Ports;

public interface IRealtimeTransactionPort
{
    Task ExecuteAsync(Guid profileId);
}
