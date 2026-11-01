namespace Wks.Interfaces;

public interface ICreateHashTag
{
    Hashtag Execute(Guid profileId, HashTagDataTransformers.Create create);
}
