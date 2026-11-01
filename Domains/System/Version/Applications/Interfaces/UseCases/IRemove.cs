namespace Wks.Interfaces;

public interface IRemoveVersion
{
    Task ExecuteAsync(Guid versionId);
}
