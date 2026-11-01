namespace Wks.UseCases;

public class GetRole(IRoleRepository _roleRepository) : IGetRole
{
    public async Task<Role> ExecuteAsync(Guid roleId) =>
        await _roleRepository
            .Include(role => role.Permissions)
            .FirstOrDefaultAsync(role =>
                role.Id == roleId && role.DeletedAt.HasValue == false
            )
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_ROLE
        );
}
