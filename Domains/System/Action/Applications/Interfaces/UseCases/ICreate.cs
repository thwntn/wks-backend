namespace Wks.Interfaces;

public interface ICreateAction
{
    List<Databases.Action> Execute(List<Databases.Action> actions);
}
