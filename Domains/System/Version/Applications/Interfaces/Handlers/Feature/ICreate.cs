namespace Wks.Interfaces;

public interface ICreateFeatureVersionHandler
{
    Task<AppFeature> ExecuteAsync(
        Guid versionId,
        VersionObject.CreateFeatureParams createParams
    );
}
