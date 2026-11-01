namespace Wks.Handlers;

public class TemplateGenerateIntelligence(
    IGenerateTextIntelligent _generateTextIntelligent,
    ITemplateIntelligence _getTemplateIntelligence
) : ITemplateGenerateIntelligence
{
    public async Task<string> ExecuteAsync(
        List<string> input,
        TemplateIntelligenceEnum templateIntelligenceEnum
    )
    {
        string prompt = _getTemplateIntelligence.Prompt(input, templateIntelligenceEnum);
        return await _generateTextIntelligent.ExecuteAsync(prompt);
    }
}
