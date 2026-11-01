namespace Wks.UseCases;

///
/// Summary:
///     Returns the latest version with its features.
///     Throws when no version exists.
///
public class CurrentVersion(IVersionRepository _versionRepository)
    : ICurrentVersion
{
    public async Task<AppVersion> ExecuteAsync() =>
        await _versionRepository
            .Include(appVersion => appVersion.Features)
            .Where(appVersion => appVersion.DeletedAt.HasValue == false)
            .OrderByDescending(appVersion => appVersion.CreatedAt)
            .FirstOrDefaultAsync()
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_VERSION
        );
}
