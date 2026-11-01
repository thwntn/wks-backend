namespace Wks.UseCases;

public class RemoveFeature(IListFeature _listFeature) : IRemoveFeature
{
    public async Task<FeatureObject.Extension> ExecuteAsync(
        Guid profileId,
        Guid featureId
    )
    {
        IEnumerable<FeatureObject.Extension> features =
            await _listFeature.ExecuteAsync();

        FeatureObject.Extension feature =
            features.FirstOrDefault(feature => feature.Id == featureId)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_FEATURE
            );

        return feature;
    }
}
