namespace Wks.Interfaces;

public interface ICreateFeatureVersion
{
    Task<AppFeature> ExecuteAsync(
        Guid versionId,
        VersionObject.CreateFeatureParams createParams
    );
}
