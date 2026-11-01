namespace Wks.Interfaces;

public interface IRemoveIcon
{
    Task<string> ExecuteAsync(Guid iconId);
}
