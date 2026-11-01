namespace Wks.UseCases;

public class GetHashTag(IHashTagRepository _tagRepository) : IGetHashTag
{
    public async Task<Hashtag> ExecuteAsync(Guid profileId, Guid tagId)
    {
        Hashtag tag = await _tagRepository.GetByIdAsync(tagId);
        if (tag.ProfileId.CompareTo(profileId) is not 0)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_TAG
            );

        return tag;
    }
}
