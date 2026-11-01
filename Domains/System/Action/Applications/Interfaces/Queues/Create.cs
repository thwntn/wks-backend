namespace Wks.Interfaces;

public interface ICreateActionPublisher
{
    Task Execute(ActionBroker.CreateActionMessage message);
}
