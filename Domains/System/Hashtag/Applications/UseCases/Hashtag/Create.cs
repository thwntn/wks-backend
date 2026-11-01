namespace Wks.UseCases;

public class CreateHashTag(IHashTagRepository _tagRepository) : ICreateHashTag
{
    public Hashtag Execute(
        Guid profileId,
        HashTagDataTransformers.Create create
    )
    {
        bool exist = _tagRepository.Raw.Any(tag =>
            tag.Name == create.Name
            && profileId == tag.ProfileId
            && tag.Filter == create.Filter
        );

        if (exist)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.TAG_ALREADY_EXISTS
            );

        Hashtag tag = create.MapTo<Hashtag>();
        tag.ProfileId = profileId;

        _tagRepository.Create(tag);
        return tag;
    }
}
