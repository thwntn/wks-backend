namespace Wks.UseCases;

public class UpdateHashTag(IHashTagRepository _tagRepository) : IUpdateHashTag
{
    public async Task<Hashtag> ExecuteAsync(
        Guid profileId,
        HashTagDataTransformers.Update update
    )
    {
        Hashtag merge = (await _tagRepository.GetByIdAsync(update.Id)).Merge(
            update
        );
        if (profileId.CompareTo(merge.ProfileId) is not 0)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_TAG
            );

        _tagRepository.Update(merge);
        return merge;
    }
}
