namespace Wks.Interfaces;

public interface IRemoveExpenseTransactionHandler
{
    Task<string> ExecuteAsync(Guid identifier, List<Guid> ids);
}
