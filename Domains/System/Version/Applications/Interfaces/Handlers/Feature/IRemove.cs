namespace Wks.Interfaces;

public interface IRemoveFeatureVersionHandler
{
    Task ExecuteAsync(Guid featureId);
}
