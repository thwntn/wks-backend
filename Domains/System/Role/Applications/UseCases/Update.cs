namespace Wks.UseCases;

public class UpdateRole(
    IRoleRepository _roleRepository,
    IPermissionRepository _rolePermissionRepository
) : IUpdateRole
{
    public async Task<Role> ExecuteAsync(
        Guid profileId,
        Guid roleId,
        RoleObject.UpdateParams updateParams
    )
    {
        Role role =
            await _roleRepository.Raw.FirstOrDefaultAsync(role =>
                role.Id == roleId && role.DeletedAt.HasValue == false
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ROLE
            );

        role.Name = updateParams.Name;
        role.Description = updateParams.Description;
        _roleRepository.Update(role);

        await ReplacePermissions(roleId, updateParams.Permissions);
        return role;
    }

    private async Task ReplacePermissions(Guid roleId, List<string> codes)
    {
        List<Permission> permissions = await _rolePermissionRepository
            .Where(permission => permission.RoleId == roleId)
            .ToListAsync();
        _rolePermissionRepository.Remove(permissions);

        _rolePermissionRepository.Create(
            codes.Select(code => new Permission
            {
                RoleId = roleId,
                Code = code,
            })
        );
    }
}
