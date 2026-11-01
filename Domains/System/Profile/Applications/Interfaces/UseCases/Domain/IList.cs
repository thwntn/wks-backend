namespace Wks.Interfaces;

public interface IListDomain
{
    Task<List<Domain>> ExecuteAsync(Guid profileId);
}
