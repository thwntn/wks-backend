namespace Wks.Interfaces;

public interface ICreateActionHandler
{
    Task<List<Databases.Action>> ExecuteAsync(List<Databases.Action> actions);
}
