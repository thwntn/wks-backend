namespace Wks.Interfaces;

public interface IUpdateHashTag
{
    Task<Hashtag> ExecuteAsync(
        Guid profileId,
        HashTagDataTransformers.Update update
    );
}
