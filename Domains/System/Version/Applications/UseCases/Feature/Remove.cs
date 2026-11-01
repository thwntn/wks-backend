namespace Wks.UseCases;

public class RemoveFeatureVersion(IFeatureRepository _featureRepository)
    : IRemoveFeatureVersion
{
    public async Task ExecuteAsync(Guid featureId)
    {
        AppFeature feature =
            await _featureRepository.Raw.FirstOrDefaultAsync(feature =>
                feature.Id == featureId && feature.DeletedAt.HasValue == false
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_VERSION
            );

        _featureRepository.Remove(feature);
    }
}
