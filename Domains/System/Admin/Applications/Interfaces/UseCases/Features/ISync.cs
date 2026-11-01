namespace Wks.Interfaces;

///
/// Summary:
///     Stages default permissions for all non-admin accounts.
///
public interface ISyncFeaturesAdmin
{
    ///
    /// Summary:
    ///     Returns account identifiers whose permissions were replaced.
    ///
    Task<List<Guid>> ExecuteAsync();
}
