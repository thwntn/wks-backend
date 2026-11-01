namespace Wks.UseCases;

public class RemoveHashTag(IHashTagRepository _tagRepository) : IRemoveHashTag
{
    public async Task<string> ExecuteAsync(Guid profileId, Guid tagId)
    {
        Hashtag tag = await _tagRepository.GetByIdAsync(tagId);
        if (profileId.CompareTo(tag.ProfileId) is not 0)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_TAG
            );

        _tagRepository.Remove(tag);
        return string.Empty;
    }
}
