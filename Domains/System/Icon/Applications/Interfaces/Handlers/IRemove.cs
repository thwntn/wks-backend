namespace Wks.Interfaces;

public interface IRemoveIconHandler
{
    Task<string> ExecuteAsync(Guid id);
}
