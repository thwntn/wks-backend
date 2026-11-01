namespace Wks.UseCases;

public class ListIcon(DatabaseContext _databaseContext) : IListIcon
{
    public async Task<IEnumerable<Icon>> ExecuteAsync()
    {
        IEnumerable<Icon> icons =
            await EntityFrameworkQueryableExtensions.ToListAsync(
                _databaseContext.Icon
            );
        return icons;
    }
}
