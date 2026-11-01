namespace Wks.Interfaces;

public interface ITypeDomain
{
    Task<List<DomainObject.Type>> ExecuteAsync();
}
