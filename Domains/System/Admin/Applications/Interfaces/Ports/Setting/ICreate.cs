namespace Wks.Interfaces;

///
/// Summary:
///     Exposes CreateSetting to administrator workflows.
/// Parameters:
///     None.
/// Returns:
///     The result supplied by the domain capability.
/// Exceptions:
///     Domain validation and infrastructure failures propagate to the caller.
///
public interface ICreateSettingAdminPort : ICreateSetting { }
