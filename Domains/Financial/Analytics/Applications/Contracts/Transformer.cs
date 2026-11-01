namespace Wks.DataTransformers;

public class FinancialAnalyticsDataTransformer
{
    public class GroupExpenseOptions
    {
        [JsonRequired]
        public DateTime StartTime { get; set; }

        [JsonRequired]
        public DateTime EndTime { get; set; }

        [JsonRequired]
        public Guid TimelineId { get; set; }
    }
}
