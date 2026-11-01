namespace Wks.Interfaces;

public interface IGetDomain
{
    Task<Domain> ExecuteAsync(Guid profileId, Guid domainId);
}
