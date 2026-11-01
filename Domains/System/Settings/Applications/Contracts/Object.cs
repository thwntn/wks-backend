namespace Wks.Objects;

///
/// Summary:
///     Response objects for system settings metadata.
///     Used by the SettingNames listing endpoint.
///
public class SettingObject
{
    ///
    /// Summary:
    ///     A setting key defined in SettingNames.
    ///
    public class Key
    {
        ///
        /// Summary:
        ///     Gets or sets the SettingNames enum value.
        ///
        /// Returns:
        ///     The numeric enum of the setting key.
        ///
        public int Enum { get; set; }

        ///
        /// Summary:
        ///     Gets or sets the setting key name.
        ///
        /// Returns:
        ///     The SettingNames member used as the setting.Name.
        ///
        public string Name { get; set; }

        ///
        /// Summary:
        ///     Gets or sets the setting key description.
        ///
        /// Returns:
        ///     A short description of what the setting stores.
        ///
        public string Description { get; set; }
    }
}
