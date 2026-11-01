namespace Wks.UseCases;

public class UnassignRole(IRoleRepository _roleRepository) : IUnassignRole
{
    public async Task<string> ExecuteAsync(
        Guid roleId,
        RoleObject.UnassignParams unassignParams
    )
    {
        Role role =
            await _roleRepository.Raw.FirstOrDefaultAsync(role =>
                role.Id == roleId
                && role.AccountId == unassignParams.AccountId
                && role.DeletedAt.HasValue == false
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ROLE
            );

        _roleRepository.SoftRemove(role, unassignParams.AccountId);

        return string.Empty;
    }
}
