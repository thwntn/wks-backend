namespace Wks.UseCases;

public class RemoveRole(IRoleRepository _roleRepository) : IRemoveRole
{
    public async Task<IEnumerable<Role>> ExecuteAsync(
        RoleObject.RemoveParams removeParams
    ) =>
        _roleRepository.Remove(
            await _roleRepository.WhereAsync(role =>
                removeParams.Ids.Contains(role.Id)
            )
        );
}
