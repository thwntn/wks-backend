namespace Wks.UseCases;

public class UpdateVersion(IVersionRepository _versionRepository)
    : IUpdateVersion
{
    public async Task<AppVersion> ExecuteAsync(
        Guid versionId,
        VersionObject.UpdateParams updateParams
    )
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

        appVersion.Name = updateParams.Name;
        appVersion.Version = updateParams.Version;
        appVersion.Reset = updateParams.Reset;
        appVersion.Highlight = updateParams.Highlight;
        appVersion.Description = updateParams.Description;

        _versionRepository.Update(appVersion);
        return appVersion;
    }
}
