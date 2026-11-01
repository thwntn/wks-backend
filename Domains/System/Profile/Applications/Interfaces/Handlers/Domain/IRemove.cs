namespace Wks.Interfaces;

public interface IRemoveDomainHandler
{
    Task<string> ExecuteAsync(Guid profileId, List<Guid> ids);
}
