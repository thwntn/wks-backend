namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to ListProfile.
///
public class ListProfileAdminAdapter(IListProfile _capability)
    : IListProfileAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     queryOptions: The search, sort and pagination options for the profile list.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Paged<Profile>> ExecuteAsync(QueryOptions queryOptions) =>
        _capability.ExecuteAsync(queryOptions);
}
