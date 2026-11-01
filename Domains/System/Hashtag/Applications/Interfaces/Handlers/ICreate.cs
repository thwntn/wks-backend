namespace Wks.Interfaces;

public interface ICreateHashTagHandler
{
    Task<Hashtag> Execute(
        Guid identifier,
        HashTagDataTransformers.Create create
    );
}
