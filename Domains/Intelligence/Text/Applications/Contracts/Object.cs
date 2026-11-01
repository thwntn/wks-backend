namespace Wks.Objects;

public class IntelligenceObject
{
    public class Prompt(string model, string role, string content)
    {
        public string Model { get; set; } = model;
        public List<Message> Messages { get; set; } = [new(role, content)];
    }

    public class Message(string role, string content)
    {
        public string Role { get; set; } = role;
        public string Content { get; set; } = content;
    }

    public class ChatCompletionResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public List<Choice> Choices { get; set; }
        public Usage Usage { get; set; }
        public string SystemFingerprint { get; set; }
        public XGroq XGroq { get; set; }
        public string ServiceTier { get; set; }
    }

    public class Choice
    {
        public int Index { get; set; }
        public ResponseMessage Message { get; set; }
        public object Logprobs { get; set; }
        public string FinishReason { get; set; }
    }

    public class ResponseMessage
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class Usage
    {
        public double QueueTime { get; set; }
        public int PromptTokens { get; set; }
        public double PromptTime { get; set; }
        public int CompletionTokens { get; set; }
        public double CompletionTime { get; set; }
        public int TotalTokens { get; set; }
        public double TotalTime { get; set; }
    }

    public class XGroq
    {
        public string Id { get; set; }
        public long Seed { get; set; }
    }

    public class TemplateGenerateParams
    {
        public List<string> Input { get; set; } = [];
        public TemplateIntelligenceEnum Template { get; set; }
    }
}
