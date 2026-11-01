namespace Wks.Interfaces;

public interface IUpdateVersionHandler
{
    Task<AppVersion> ExecuteAsync(
        Guid versionId,
        VersionObject.UpdateParams updateParams
    );
}
