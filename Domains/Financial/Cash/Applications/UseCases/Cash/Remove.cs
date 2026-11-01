namespace Wks.UseCases;

public class RemoveCash(ICashRepository _cashRepository, ISocket _socketService)
    : IRemoveCash
{
    public async Task<string> ExecuteAsync(Guid profileId, Guid cashId)
    {
        Cash cash =
            await _cashRepository.Raw.FirstOrDefaultAsync(cash =>
                cash.Id == cashId && cash.ProfileId == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_CASH
            );

        _cashRepository.Remove(cash);

        await _socketService.InvokeAsync(
            profileId,
            nameof(HubMethodName.CASH),
            nameof(Objects)
        );
        return string.Empty;
    }
}
