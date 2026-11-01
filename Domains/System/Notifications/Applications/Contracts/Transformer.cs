namespace Wks.DataTransformers;

public class NotificationDataTransformers
{
    public class SystemPush
    {
        [JsonRequired]
        public string Title { get; set; }

        [JsonRequired]
        public string Description { get; set; }

        [JsonRequired]
        public string Url { get; set; }
    }

    public class QueryOptions : Objects.QueryOptions
    {
        ///
        /// Summary:
        ///     Gets or sets the notification types to filter by.
        ///
        /// Returns:
        ///     The notification types to include; null when no type filter is
        ///     applied, or an empty list to return no records.
        ///
        public List<NotificationNames> Types { get; set; }
    }
}
