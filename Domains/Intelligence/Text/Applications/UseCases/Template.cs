namespace Wks.UseCases;

/// <summary>Builds the Financial transaction description prompt using the migrated ECMC template.</summary>
public class TemplateIntelligence : ITemplateIntelligence
{
    /// <summary>Replaces input placeholders; rejects templates that do not belong to Financial.</summary>
    public string Prompt(List<string> input, TemplateIntelligenceEnum templateIntelligenceEnum)
    {
        if (templateIntelligenceEnum != TemplateIntelligenceEnum.FINANCIAL_TRANSACTION_DESCRIPTION)
            throw new HttpException(HttpStatus.BadRequest, MessageConstant.UNSUPPORTED_FINANCIAL_TEMPLATE);
        string mapping = PromptConstant.FINANCIAL_TRANSACTION_DESCRIPTION;
        for (int index = 0; index < input.Count; index++)
            mapping = mapping.Replace(Format.ToCacheName(nameof(input), string.Concat(index)), input[index]);
        return mapping;
    }
}
