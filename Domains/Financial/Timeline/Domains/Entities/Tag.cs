namespace Wks.Databases;

///
/// Summary:
///     Represents the many-to-many relationship between financial timelines and hashtags.
///
[PrimaryKey(nameof(TimelineId), nameof(TagId))]
public class TimelineTag(Guid timelineId, Guid tagId)
{
    ///
    /// Summary:
    ///     Gets or sets the timeline identifier.
    ///
    /// Returns:
    ///     The unique identifier of the timeline in this relationship.
    ///
    [Required]
    public Guid TimelineId { get; set; } = timelineId;

    [ForeignKey(nameof(TimelineId))]
    public Timeline Timeline { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the hashtag identifier.
    ///
    /// Returns:
    ///     The unique identifier of the hashtag in this relationship.
    ///
    [Required]
    public Guid TagId { get; set; } = tagId;

    [ForeignKey(nameof(TagId))]
    public Hashtag Tag { get; set; }
}
