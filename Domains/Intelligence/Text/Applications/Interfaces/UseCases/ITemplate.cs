namespace Wks.Interfaces;

public interface ITemplateIntelligence
{
    string Prompt(List<string> input, TemplateIntelligenceEnum templateIntelligenceEnum);
}
