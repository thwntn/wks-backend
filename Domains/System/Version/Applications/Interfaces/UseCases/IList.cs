namespace Wks.Interfaces;

public interface IListVersion
{
    Task<List<AppVersion>> ExecuteAsync();
}
