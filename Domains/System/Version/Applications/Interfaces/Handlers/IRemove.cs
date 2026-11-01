namespace Wks.Interfaces;

public interface IRemoveVersionHandler
{
    Task ExecuteAsync(Guid versionId);
}
