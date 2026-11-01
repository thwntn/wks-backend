namespace Wks.Interfaces;

public interface IRemoveFeature
{
    Task<FeatureObject.Extension> ExecuteAsync(Guid profile, Guid featureId);
}
