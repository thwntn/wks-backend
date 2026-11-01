namespace Wks.UseCases;

public class ListVersion(IVersionRepository _versionRepository) : IListVersion
{
    public async Task<List<AppVersion>> ExecuteAsync() =>
        await _versionRepository
            .Include(appVersion => appVersion.Features)
            .Where(appVersion => appVersion.DeletedAt.HasValue == false)
            .OrderByDescending(appVersion => appVersion.CreatedAt)
            .ToListAsync();
}
