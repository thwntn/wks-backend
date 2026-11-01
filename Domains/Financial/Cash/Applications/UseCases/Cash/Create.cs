namespace Wks.UseCases;

public class CreateCash(ICashRepository _cashRepository, ISocket _socketService) : ICreateCash
{
    public async Task<Cash> ExecuteAsync(
        Guid profileId,
        Guid fundId,
        CashDataTransformer.Create create
    )
    {
        // Fund check removed

        Cash cash = new(create.Name, create.Quantity) { ProfileId = profileId };
        _cashRepository.Create(cash);

        await _socketService.InvokeAsync(profileId, nameof(HubMethodName.CASH), nameof(Objects));
        return cash;
    }
}
