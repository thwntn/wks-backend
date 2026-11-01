namespace Wks.Interfaces;

public interface IRemoveHashTag
{
    Task<string> ExecuteAsync(Guid profileId, Guid tagId);
}
