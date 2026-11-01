namespace Wks.Interfaces;

public interface ICreateCashDescription
{
    CashDescription Execute(Guid cashId, CashDescriptionDataTransformer.Create create);
}
