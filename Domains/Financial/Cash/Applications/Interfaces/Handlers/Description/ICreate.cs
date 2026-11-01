namespace Wks.Interfaces;

public interface ICreateCashDescriptionHandler
{
    Task<CashDescription> ExecuteAsync(Guid id, CashDescriptionDataTransformer.Create create);
}
