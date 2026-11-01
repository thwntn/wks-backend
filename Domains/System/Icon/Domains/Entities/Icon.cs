namespace Wks.Databases;

///
/// Summary:
///     Represents an icon asset associated with a profile for storefront customization.
///
public class Icon(string name, string description, string directory)
    : Entity,
        IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the icon display name.
    ///
    /// Returns:
    ///     The human-readable name for identifying this icon.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the icon description.
    ///
    /// Returns:
    ///     A brief description of the icon's purpose or usage.
    ///
    [Required]
    public string Description { get; set; } = description;

    ///
    /// Summary:
    ///     Gets or sets the directory path where this icon is stored.
    ///
    /// Returns:
    ///     The storage directory path for this icon file.
    ///
    [Required]
    public string Directory { get; set; } = directory;

    ///
    /// Summary:
    ///     Gets or sets the optional profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile this icon belongs to, or null for system icons.
    ///
    [AllowNull]
    public Guid? ProfileId { get; set; }

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }
}
