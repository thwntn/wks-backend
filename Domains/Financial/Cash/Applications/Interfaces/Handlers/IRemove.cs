namespace Wks.Interfaces;

public interface IRemoveCashHandler
{
    Task<string> ExecuteAsync(Guid identifier, Guid id);
}
