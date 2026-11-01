namespace Wks.UseCases;

public class CreateVersion(IVersionRepository _versionRepository)
    : ICreateVersion
{
    public async Task<AppVersion> ExecuteAsync(
        VersionObject.CreateParams createParams
    )
    {
        bool exists = await _versionRepository.Raw.AnyAsync(appVersion =>
            appVersion.Version == createParams.Version
            && appVersion.DeletedAt.HasValue == false
        );

        if (exists)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.VERSION_ALREADY_EXISTS
            );

        AppVersion appVersion = new(createParams.Name, createParams.Version)
        {
            Reset = createParams.Reset,
            Highlight = createParams.Highlight,
            Description = createParams.Description,
        };

        _versionRepository.Create(appVersion);
        return appVersion;
    }
}
