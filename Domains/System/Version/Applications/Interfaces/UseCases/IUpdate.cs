namespace Wks.Interfaces;

public interface IUpdateVersion
{
    Task<AppVersion> ExecuteAsync(
        Guid versionId,
        VersionObject.UpdateParams updateParams
    );
}
