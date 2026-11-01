namespace Wks.Interfaces;

public interface IResolveDomain
{
    Task<Guid> ExecuteAsync(string from);
}
