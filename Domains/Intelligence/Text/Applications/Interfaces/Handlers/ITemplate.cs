namespace Wks.Interfaces;

public interface ITemplateGenerateIntelligence
{
    Task<string> ExecuteAsync(
        List<string> input,
        TemplateIntelligenceEnum templateIntelligenceEnum
    );
}
