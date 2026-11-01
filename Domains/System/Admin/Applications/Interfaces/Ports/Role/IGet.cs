namespace Wks.Interfaces;

///
/// Summary:
///     Exposes GetRole to administrator workflows.
/// Parameters:
///     None.
/// Returns:
///     The result supplied by the domain capability.
/// Exceptions:
///     Domain validation and infrastructure failures propagate to the caller.
///
public interface IGetRoleAdminPort : IGetRole { }
