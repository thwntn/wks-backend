namespace Wks.DataTransformers;

public class TimelineDataTransformer
{
    public class Create
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Description { get; set; }

        [JsonRequired]
        public DateTime FromDate { get; set; }

        [JsonRequired]
        public DateTime ToDate { get; set; }

        [JsonRequired]
        public List<Guid> TagIds { get; set; }
    }

    public class Update : Create
    {
        [JsonRequired]
        public Guid Id { get; set; }
    }

    public class Clone
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
    }

    public class UpdateTag
    {
        [JsonRequired]
        public List<Guid> TagIds { get; set; }
    }

    public class TimelineResponse
    {
        [NotMapped]
        public Guid Id;

        [NotMapped]
        public string Name;

        [NotMapped]
        public string Description;

        [NotMapped]
        public DateTime FromDate;

        [NotMapped]
        public DateTime ToDate;

        [NotMapped]
        public List<TimelineTag> TimelineTags;

        [NotMapped]
        public DateTime DateTime;

        [NotMapped]
        public long IsPinned;

        [NotMapped]
        public long Income;

        [NotMapped]
        public long Outcome;

        [NotMapped]
        public long QuantityTransaction;
    }
}
