namespace Wks.UseCases;

public class RemoveIcon(IIconRepository _iconRepository) : IRemoveIcon
{
    public async Task<string> ExecuteAsync(Guid iconId)
    {
        Icon icon =
            await _iconRepository.Raw.FirstOrDefaultAsync(icon =>
                icon.Id == iconId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ICON
            );
        _iconRepository.Remove(icon);
        return string.Empty;
    }
}
