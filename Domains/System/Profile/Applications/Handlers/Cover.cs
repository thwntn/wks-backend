namespace Wks.Handlers;

public class ChangeCoverPictureHandler(
    IChangeCoverPictureProfile _changeCoverPicture,
    IUnitOfWork _unitOfWork
) : IChangeCoverPictureHandler
{
    public async Task<Account> ExecuteAsync(
        Guid profileId,
        string artifactToken
    )
    {
        Account result = await _changeCoverPicture.ExecuteAsync(
            profileId,
            artifactToken
        );
        await _unitOfWork.SyncAsync();
        return result;
    }
}
