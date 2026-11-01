namespace Wks.UseCases;

public class StaticArtifact : IStaticArtifact
{
    private const string PATH = "Shared/Metadata/Static";

    public async Task<MemoryStream> ExecuteAsync(string fileName) =>
        new(
            await File.ReadAllBytesAsync(
                Format.ToPathName(
                    Directory.GetCurrentDirectory(),
                    PATH,
                    fileName
                )
            )
        );
}
