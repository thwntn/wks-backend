namespace Wks.Handlers;

public class UpdateProfileHandler(
    IUpdateProfile _updateProfile,
    IUnitOfWork _unitOfWork
) : IUpdateProfileHandler
{
    public async Task<Account> ExecuteAsync(
        Guid profileId,
        ProfileDataTransformer.Update update
    )
    {
        Account result = _updateProfile.Execute(profileId, update);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
