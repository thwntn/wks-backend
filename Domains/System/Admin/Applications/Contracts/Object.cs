namespace Wks.Objects;

///
/// Summary:
///     Contains internal data for administrator domain workflows.
///
public class DomainAdminObject
{
    ///
    /// Summary:
    ///     Carries the host and routing type selected for domain setup.
    ///
    public class SetupParams
    {
        ///
        /// Summary:
        ///     Gets or sets the requested domain host.
        /// Returns:
        ///     The host passed to the profile domain capability.
        ///
        public string Host { get; set; }

        ///
        /// Summary:
        ///     Gets or sets the domain routing type.
        /// Returns:
        ///     The shop or redirect routing type requested by the administrator.
        ///
        public DomainTypeEnum Type { get; set; }
    }
}
