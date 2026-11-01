namespace Wks.Interfaces;

public interface IRemoveExpenseHandler
{
    Task<string> ExecuteAsync(Guid identifier, IEnumerable<Guid> expenseIds);
}
