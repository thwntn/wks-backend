namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to TypeDomain.
///
public class TypeDomainAdminAdapter(ITypeDomain _capability)
    : ITypeDomainAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     None.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<List<DomainObject.Type>> ExecuteAsync() =>
        _capability.ExecuteAsync();
}
