namespace Wks.Interfaces;

public interface IListFeature
{
    Task<List<FeatureObject.Extension>> ExecuteAsync();
}
