namespace Wks.UseCases;

public class CreateIcon(IIconRepository _iconRepository) : ICreateIcon
{
    public Icon Execute(IconDataTransformer.Create create)
    {
        Icon icon = create.MapTo<Icon>();
        icon.ProfileId = null;
        _iconRepository.Create(icon);
        return icon;
    }
}
