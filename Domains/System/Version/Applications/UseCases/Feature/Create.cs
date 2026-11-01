namespace Wks.UseCases;

public class CreateFeatureVersion(
    IVersionRepository _versionRepository,
    IFeatureRepository _featureRepository
) : ICreateFeatureVersion
{
    public async Task<AppFeature> ExecuteAsync(
        Guid versionId,
        VersionObject.CreateFeatureParams createParams
    )
    {
        bool versionExists = await _versionRepository.Raw.AnyAsync(appVersion =>
            appVersion.Id == versionId && appVersion.DeletedAt.HasValue == false
        );

        if (versionExists is false)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_VERSION
            );

        AppFeature feature = new(createParams.Title)
        {
            Description = createParams.Description,
            VersionId = versionId,
        };

        _featureRepository.Create(feature);
        return feature;
    }
}
