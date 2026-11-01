namespace Wks.Interfaces;

public interface IAddFeatureHandler
{
    Task<FeatureObject.Extension> ExecuteAsync(Guid identifier, Guid featureId);
}
