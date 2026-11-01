namespace Wks.UseCases;

public class CreateRole(
    IRoleRepository _roleRepository,
    IPermissionRepository _rolePermissionRepository
) : ICreateRole
{
    public async Task<Role> ExecuteAsync(RoleObject.CreateParams createParams)
    {
        bool exists = await _roleRepository.Raw.AnyAsync(role =>
            role.AccountId == createParams.AccountId
            && role.Code == createParams.Code
            && role.DeletedAt.HasValue == false
        );

        if (exists)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.ROLE_ALREADY_EXISTS
            );

        Role role = new(createParams.Name, createParams.Code)
        {
            Description = createParams.Description,
            AccountId = createParams.AccountId,
        };

        _roleRepository.Create(role);
        _rolePermissionRepository.Create(
            createParams.Permissions.Select(code => new Permission
            {
                RoleId = role.Id,
                Code = code,
            })
        );

        return role;
    }
}
