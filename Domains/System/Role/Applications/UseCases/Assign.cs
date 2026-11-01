namespace Wks.UseCases;

public class AssignRole(
    IRoleRepository _roleRepository,
    IPermissionRepository _permissionRepository
) : IAssignRole
{
    public async Task<Role> ExecuteAsync(
        Guid roleId,
        RoleObject.AssignParams assignParams
    )
    {
        Role sourceRole =
            await _roleRepository
                .Include(role => role.Permissions)
                .FirstOrDefaultAsync(role =>
                    role.Id == roleId && role.DeletedAt.HasValue == false
                )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ROLE
            );

        bool alreadyAssigned = await _roleRepository.Raw.AnyAsync(role =>
            role.AccountId == assignParams.AccountId
            && role.Code == sourceRole.Code
            && role.DeletedAt.HasValue == false
        );

        if (alreadyAssigned)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.ALREADY_ASSIGN_ROLE
            );

        Role assignedRole = new(sourceRole.Name, sourceRole.Code)
        {
            Description = sourceRole.Description,
            AccountId = assignParams.AccountId,
        };

        _roleRepository.Create(assignedRole);

        _permissionRepository.Create(
            sourceRole.Permissions?.Select(permission => new Permission
            {
                RoleId = assignedRole.Id,
                Code = permission.Code,
            }) ?? []
        );

        return assignedRole;
    }
}
