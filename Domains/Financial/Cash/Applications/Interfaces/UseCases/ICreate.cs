namespace Wks.Interfaces;

public interface ICreateCash
{
    Task<Cash> ExecuteAsync(Guid profileId, Guid fundId, CashDataTransformer.Create create);
}
