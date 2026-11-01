namespace Wks.UseCases;

public class ListFeature : IListFeature
{
    public async Task<List<FeatureObject.Extension>> ExecuteAsync()
    {
        string define = await File.ReadAllTextAsync(
            $"{Directory.GetCurrentDirectory()}/Shared/Metadata/Master/System/Features.JSON"
        );

        List<FeatureObject.Extension> apps = define.Deserialize<
            List<FeatureObject.Extension>
        >();
        return apps;
    }
}
