namespace Wks.Mains;
///
/// Summary:
///     Configures RabbitMQ consumers used by the WKS platform.
///
public class RabbitMQ
{
    ///
    /// Summary:
    ///     Registers account provisioning and audit action consumers on the supplied bus configurator.
    ///
    public static void Configure(IBusRegistrationConfigurator setting)
    {
        setting.AddConsumer<SetupAccountAuthConsumer>();
        setting.AddConsumer<CreateActionConsumer>();
    }
}
