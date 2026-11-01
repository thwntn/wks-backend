namespace Wks.DataTransformers;

///
/// Summary:
///     Request payloads for creating, updating, and removing system settings.
///     Used by the settings API and internal upsert flows.
///
public class SettingDataTransformer
{
    ///
    /// Summary:
    ///     Payload for creating or upserting a setting by name.
    ///
    public class Create
    {
        ///
        /// Summary:
        ///     Gets or sets the unique setting name.
        ///
        /// Returns:
        ///     The configuration key stored on the setting.
        ///
        [JsonRequired]
        public string Name { get; set; }

        ///
        /// Summary:
        ///     Gets or sets the setting value.
        ///
        /// Returns:
        ///     The configuration value stored on the setting.
        ///
        [JsonRequired]
        public string Value { get; set; }
    }

    ///
    /// Summary:
    ///     Payload for updating an existing setting by identifier.
    ///
    public class Update : Create
    {
        ///
        /// Summary:
        ///     Gets or sets the identifier of the setting to update.
        ///
        /// Returns:
        ///     The unique identifier of the target setting.
        ///
        [JsonRequired]
        public Guid Id { get; set; }
    }

    ///
    /// Summary:
    ///     Payload for soft-deleting one or more settings.
    ///
    public class Remove
    {
        ///
        /// Summary:
        ///     Gets or sets the setting identifiers to remove.
        ///
        /// Returns:
        ///     The list of setting identifiers to soft-delete.
        ///
        [JsonRequired]
        public List<Guid> Ids { get; set; }
    }
}
