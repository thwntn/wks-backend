namespace Wks.Objects;

public class ActionBroker
{
    public class CreateActionMessage(List<Databases.Action> actions)
    {
        public List<Databases.Action> Actions { get; set; } = actions;
    }
}
