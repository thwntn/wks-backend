namespace Wks.Interfaces;

public interface IUpdateHashTagHandler
{
    Task<Hashtag> ExecuteAsync(
        Guid identifier,
        HashTagDataTransformers.Update update
    );
}
