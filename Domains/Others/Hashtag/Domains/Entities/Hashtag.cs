namespace Wks.Databases;

///
/// Summary:
///     Represents a hashtag used to categorize and label entities across the system.
///
public class Hashtag : Entity, IEntity
{
    private static readonly int INITIAL = 0;

    ///
    /// Summary:
    ///     Gets or sets the hashtag name.
    ///
    /// Returns:
    ///     The tag name used for categorization and filtering.
    ///
    [Required]
    public string Name { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the display color for this hashtag.
    ///
    /// Returns:
    ///     The color value used to visually distinguish this tag.
    ///
    [Required]
    public string Color { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the filter usage count for this hashtag.
    ///
    /// Returns:
    ///     The number of times this hashtag has been used in filters.
    ///
    [Required]
    public int Filter { get; set; } = INITIAL;

    ///
    /// Summary:
    ///     Gets or sets the profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile that owns this hashtag.
    ///
    [Required]
    public Guid ProfileId { get; set; }

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }

    #region Reference
    #endregion
}
