namespace Wks.UseCases;

public class AddFeature(
    IListFeature _listFeature,
    IProfileRepository _profileRepository
) : IAddFeature
{
    public async Task<FeatureObject.Extension> ExecuteAsync(
        Guid profileId,
        Guid featureId
    )
    {
        Profile profile =
            await _profileRepository.FirstOrDefaultAsync(p => p.Id == profileId)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_PROFILE
            );

        FeatureObject.Extension app =
            (await _listFeature.ExecuteAsync()).FirstOrDefault(feature =>
                feature.Id == featureId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_FEATURE
            );

        return app;
    }
}
