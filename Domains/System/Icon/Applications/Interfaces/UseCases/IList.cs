namespace Wks.Interfaces;

public interface IListIcon
{
    Task<IEnumerable<Icon>> ExecuteAsync();
}
