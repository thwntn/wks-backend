namespace Wks.UseCases;

public class ListRoles(IRoleRepository _roleRepository) : IListRoles
{
    public async Task<List<Role>> ExecuteAsync(Guid accountId) =>
        await _roleRepository
            .Include(role => role.Permissions)
            .Where(role => role.AccountId == accountId)
            .OrderByDescending(role => role.CreatedAt)
            .ToListAsync();
}
