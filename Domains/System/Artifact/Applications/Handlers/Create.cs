namespace Wks.Handlers;

public class CreateArtifactHandler(
    ICreateArtifact _createArtifact,
    IUnitOfWork _unitOfWork
) : ICreateArtifactHandler
{
    public async Task<Artifact> ExecuteAsync(ArtifactParams.Create createParams)
    {
        Artifact artifact = await _createArtifact.ExecuteAsync(createParams);
        await _unitOfWork.SyncAsync();
        return artifact;
    }
}
