namespace Wks.Interfaces;

public interface IRemoveExpense
{
    Task<string> ExecuteAsync(Guid profileId, IEnumerable<Guid> expenseIds);
}
