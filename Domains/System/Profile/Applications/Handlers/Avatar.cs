namespace Wks.Handlers;

public class ChangeAvatarHandler(
    IChangeAvatarProfile _changeAvatar,
    IUnitOfWork _unitOfWork
) : IChangeAvatarHandler
{
    public async Task<Account> ExecuteAsync(
        Guid profileId,
        string artifactToken
    )
    {
        Account result = await _changeAvatar.ExecuteAsync(
            profileId,
            artifactToken
        );
        await _unitOfWork.SyncAsync();
        return result;
    }
}
