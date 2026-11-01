namespace Wks.Interfaces;

public interface ICreateCashHandler
{
    Task<Cash> ExecuteAsync(Guid identifier, Guid id, CashDataTransformer.Create create);
}
