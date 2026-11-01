namespace Wks.UseCases;

public class CreateAction(IActionRepository _actionRepository) : ICreateAction
{
    public List<Databases.Action> Execute(List<Databases.Action> actions)
    {
        List<string> names = Enum.GetNames(typeof(ActionNames)).ToList();

        bool contain = actions.All(action => names.Contains(action.Type));
        if (contain is false)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACTION_TYPE
            );

        /// Summary:
        ///    Creates new actions in the system.
        /// Actions:
        ///         A list of action objects containing the details of the actions to be created.
        return _actionRepository
            .Create(
                actions.Select(action => new Databases.Action(
                    action.ProfileId,
                    action.Type,
                    action.JsonData
                ))
            )
            .ToList();
    }
}
