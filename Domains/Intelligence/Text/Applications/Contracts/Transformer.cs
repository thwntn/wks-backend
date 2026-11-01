using System.ComponentModel.DataAnnotations;

namespace Wks.DataTransformers;

public class IntelligenceTextDataTransformer
{
    public class Generate
    {
        [Required]
        public string Prompt { get; set; }
    }

    public class TemplateGenerate
    {
        [Required]
        public List<string> Input { get; set; }

        [Required]
        public TemplateIntelligenceEnum Template { get; set; }
    }
}
