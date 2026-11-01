namespace Wks.DataTransformers;

public class DomainDataTransformer
{
    public class Create
    {
        [JsonRequired]
        public string Host { get; set; }

        [JsonRequired]
        public DomainTypeEnum Type { get; set; }
    }

    public class Update : Create
    {
        [JsonRequired]
        public Guid Id { get; set; }
    }

    public class Remove
    {
        [JsonRequired]
        public List<Guid> Ids { get; set; }
    }
}
