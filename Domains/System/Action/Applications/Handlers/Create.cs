namespace Wks.Handlers;

public class CreateActionHandler(
    ICreateAction _createAction,
    IUnitOfWork _unitOfWork
) : ICreateActionHandler
{
    public async Task<List<Databases.Action>> ExecuteAsync(
        List<Databases.Action> actions
    )
    {
        List<Databases.Action> result = _createAction.Execute(actions);
        await _unitOfWork.SyncAsync();
        return actions;
    }
}
