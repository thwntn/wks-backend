namespace Wks.Interfaces;

public interface IGetHashTag
{
    Task<Hashtag> ExecuteAsync(Guid profileId, Guid tagId);
}
