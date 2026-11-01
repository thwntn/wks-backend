namespace Wks.UseCases;

public class PermissionRole(ICache _cache) : IPermissionRole
{
    public async Task<List<RoleObject.PermissionMaster>> ExecuteAsync()
    {
        string cacheKey = nameof(CacheNames.SYSTEM_ROLE_PERMISSION);
        string json = await _cache.CacheAsync(
            cacheKey,
            () =>
                File.ReadAllTextAsync(
                    $"{Directory.GetCurrentDirectory()}/Shared/Metadata/Master/System/Permission.JSON"
                )
        );

        return json.Deserialize<List<RoleObject.PermissionMaster>>();
    }
}
