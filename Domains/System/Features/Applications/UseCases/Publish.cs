namespace Wks.UseCases;

public class PublishFeature(
    IListFeature _listFeature
) : IPublishFeature
{
    public async Task ExecuteAsync(string code)
    {
        List<FeatureObject.Extension> extensions = await _listFeature.ExecuteAsync();
        FeatureObject.Extension feature =
            extensions.FirstOrDefault(extension => extension.Enum == code)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_FEATURE
            );

        // Feature published — permissions are now managed via custom roles (PerrmissionContant enum).
        // No longer needed to assign Permission entities to accounts.
    }
}
