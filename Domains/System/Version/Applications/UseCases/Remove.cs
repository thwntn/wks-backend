namespace Wks.UseCases;

public class RemoveVersion(IVersionRepository _versionRepository)
    : IRemoveVersion
{
    public async Task ExecuteAsync(Guid versionId)
    {
        AppVersion appVersion =
            await _versionRepository.Raw.FirstOrDefaultAsync(appVersion =>
                appVersion.Id == versionId
                && appVersion.DeletedAt.HasValue == false
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_VERSION
            );

        _versionRepository.Remove(appVersion);
    }
}
