namespace Wks.Interfaces;

public interface IRemoveFeatureVersion
{
    Task ExecuteAsync(Guid featureId);
}
