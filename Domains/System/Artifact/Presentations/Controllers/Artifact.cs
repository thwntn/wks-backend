namespace Wks.Controllers;

[ApiController, Route(RouteConstant.ARTIFACT), Tags(SwaggerConstant.ARTIFACT)]
public class ArtifactController(
    IPresignedUploadArtifact _presignedUploadArtifact,
    IDataStreamArtifact _getStreamArtifact,
    ICreateArtifactHandler _createArtifact,
    IStaticArtifact _staticArtifact
) : ControllerBase
{
    [HttpGet(RouteConstant.ARTIFACT_SESSION)]
    public async Task<IActionResult> PresignedUpload()
    {
        ArtifactObject.Presigned presigned =
            await _presignedUploadArtifact.ExecuteAsync();
        return Ok(presigned);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ArtifactDataTransformer.Create createDataTransformer
    )
    {
        Artifact artifact = await _createArtifact.ExecuteAsync(
            createDataTransformer.MapTo<ArtifactParams.Create>()
        );
        return Ok(artifact);
    }

    [
        HttpGet(RouteConstant.ARTIFACT_TOKEN),
        HttpHead(RouteConstant.ARTIFACT_TOKEN)
    ]
    public async Task<IActionResult> Get(
        [FromRoute] string token,
        [FromRoute] string fileName
    )
    {
        ArtifactObject.Stream stream = await _getStreamArtifact.ExecuteAsync(
            token
        );
        string rawName = stream.Artifact.RawName;

        if (fileName.Justifiable())
            rawName = fileName;

        return File(
            stream.MemoryStream,
            global::Shared.Extension.OCTEL_STREAM,
            rawName
        );
    }

    [HttpGet(RouteConstant.ARTIFACT_STATIC)]
    public async Task<IActionResult> Access([FromRoute] string fileName)
    {
        MemoryStream memoryStream = await _staticArtifact.ExecuteAsync(
            fileName
        );

        if (memoryStream.Justifiable() is false)
            return File(
                Encoding.UTF8.GetBytes(string.Empty),
                global::Shared.Extension.OCTEL_STREAM
            );

        return File(memoryStream, global::Shared.Extension.OCTEL_STREAM);
    }
}
