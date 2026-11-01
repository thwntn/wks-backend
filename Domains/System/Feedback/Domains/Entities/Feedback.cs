namespace Wks.Databases;

///
/// Summary:
///     Represents a feedback submission from a profile with contact and content details.
///
public class Feedback(
    Guid profileId,
    string name,
    string code,
    string contact,
    string content
) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the submitter display name.
    ///
    /// Returns:
    ///     The name of the person submitting the feedback.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the feedback reference code.
    ///
    /// Returns:
    ///     The unique reference code for tracking this feedback submission.
    ///
    [Required]
    public string Code { get; set; } = code;

    ///
    /// Summary:
    ///     Gets or sets the contact information of the submitter.
    ///
    /// Returns:
    ///     The contact string (email or phone) provided for follow-up.
    ///
    [Required]
    public string Contact { get; set; } = contact;

    ///
    /// Summary:
    ///     Gets or sets the main feedback content.
    ///
    /// Returns:
    ///     The body text of the feedback submission.
    ///
    [Required]
    public string Content { get; set; } = content;

    ///
    /// Summary:
    ///     Gets or sets the optional file attachment token.
    ///
    /// Returns:
    ///     Optional file token for an attachment, or null if no file is attached.
    ///
    [AllowNull]
    public string? File { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the feedback lifecycle status.
    ///
    /// Returns:
    ///     The status enum indicating the current state of this feedback.
    ///
    [Required]
    public FeedbackStatusEnum Status { get; set; } = FeedbackStatusEnum.Initial;

    ///
    /// Summary:
    ///     Gets or sets whether this feedback is pinned for quick access.
    ///
    /// Returns:
    ///     True if the feedback is pinned; false otherwise.
    ///
    [Required]
    public bool IsPinned { get; set; } = false;

    ///
    /// Summary:
    ///     Gets or sets the profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile that submitted this feedback.
    ///
    [Required]
    public Guid ProfileId { get; set; } = profileId;

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }
}
