namespace Wks.UseCases;

public class GenerateTextIntelligent(IRequest _request) : IGenerateTextIntelligent
{
    public async Task<string> ExecuteAsync(string content)
    {
        IntelligenceObject.Prompt prompt = new(
            Runtime.IntelligenceModel.Env(),
            Runtime.IntelligenceRole.Env(),
            content
        );

        HttpObject.Response<IntelligenceObject.ChatCompletionResponse> chatCompletionResponse =
            await _request.JsonAsync<IntelligenceObject.ChatCompletionResponse>(
                Runtime.IntelligenceApi.Env(),
                prompt,
                new() { [nameof(Authorization)] = Runtime.IntelligenceApiKey.Env() }
            );

        if (
            chatCompletionResponse.IsSuccessStatusCode is false
            || chatCompletionResponse.Data.Choices.Count is 0
        )
            return default;

        return chatCompletionResponse.Data.Choices.First().Message.Content;
    }
}
