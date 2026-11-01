namespace Wks.Interfaces;

public interface IRemoveFeatureHandler
{
    Task<FeatureObject.Extension> ExecuteAsync(Guid identifier, Guid id);
}
