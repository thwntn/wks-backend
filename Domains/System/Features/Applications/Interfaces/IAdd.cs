namespace Wks.Interfaces;

public interface IAddFeature
{
    public Task<FeatureObject.Extension> ExecuteAsync(
        Guid profile,
        Guid featureId
    );
}
