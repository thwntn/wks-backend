namespace Wks.UseCases;

public class ListStatusFeedback(ICache _cache) : IListStatusFeedback
{
    public async Task<List<FeedbackObject.Status>> ExecuteAsync()
    {
        string define = await _cache.CacheAsync(
            nameof(ListStatusFeedback),
            () =>
                File.ReadAllTextAsync(
                    $"{Directory.GetCurrentDirectory()}/Shared/Metadata/Master/System/FeedbackStatus.JSON"
                )
        );

        return define.Deserialize<List<FeedbackObject.Status>>();
    }
}
