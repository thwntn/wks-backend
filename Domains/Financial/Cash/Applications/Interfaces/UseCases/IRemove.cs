namespace Wks.Interfaces;

public interface IRemoveCash
{
    Task<string> ExecuteAsync(Guid profileId, Guid cashId);
}
