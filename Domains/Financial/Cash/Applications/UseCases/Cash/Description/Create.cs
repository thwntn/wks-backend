namespace Wks.UseCases;

public class CreateCashDescription(ICashDescriptionRepository _cashDescriptionRepository)
    : ICreateCashDescription
{
    public CashDescription Execute(Guid cashId, CashDescriptionDataTransformer.Create create)
    {
        CashDescription cashDescription = new(create.Name, create.Quantity)
        {
            CashNames = create.CashNames,
            CashId = cashId,
        };
        _cashDescriptionRepository.Create(cashDescription);
        return cashDescription;
    }
}
