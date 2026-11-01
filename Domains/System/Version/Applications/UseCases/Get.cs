namespace Wks.UseCases;

public class GetVersion(IVersionRepository _versionRepository) : IGetVersion
{
    public async Task<AppVersion> ExecuteAsync(Guid versionId) =>
        await _versionRepository
            .Include(appVersion => appVersion.Features)
            .FirstOrDefaultAsync(appVersion =>
                appVersion.Id == versionId
                && appVersion.DeletedAt.HasValue == false
            )
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_VERSION
        );
}
