namespace Wks.Databases;

///
/// Summary:
///     Represents a system-wide configuration setting entity with name-value storage.
///     Settings are not scoped to a profile and support soft delete.
///
public class Setting(string name, string value) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the unique setting name.
    ///
    /// Returns:
    ///     The configuration key used to look up this setting.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the setting value.
    ///
    /// Returns:
    ///     The configuration value stored for this name.
    ///
    [Required]
    public string Value { get; set; } = value;
}
