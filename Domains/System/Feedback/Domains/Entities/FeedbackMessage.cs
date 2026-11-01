namespace Wks.Databases;

///
/// Summary:
///     Represents a message within a feedback thread, sent by a profile.
///
public class FeedbackMessage(
    Guid profileId,
    Guid feedbackId,
    string message,
    string file
) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the message text content.
    ///
    /// Returns:
    ///     The text body of the feedback message.
    ///
    [Required]
    public string Message { get; set; } = message;

    ///
    /// Summary:
    ///     Gets or sets the optional file attachment token.
    ///
    /// Returns:
    ///     Optional file token for an attachment, or null if no file is attached.
    ///
    [AllowNull]
    public string? File { get; set; } = file;

    ///
    /// Summary:
    ///     Gets or sets the profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile that sent this message.
    ///
    [Required]
    public Guid ProfileId { get; set; } = profileId;

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the feedback thread identifier.
    ///
    /// Returns:
    ///     The unique identifier of the feedback thread this message belongs to.
    ///
    [Required]
    public Guid FeedbackId { get; set; } = feedbackId;

    [ForeignKey(nameof(FeedbackId))]
    public Feedback Feedback { get; set; }
}
