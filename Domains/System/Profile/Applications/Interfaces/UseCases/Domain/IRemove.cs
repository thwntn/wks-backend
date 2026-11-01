namespace Wks.Interfaces;

public interface IRemoveDomain
{
    Task ExecuteAsync(Guid profileId, List<Guid> ids);
}
