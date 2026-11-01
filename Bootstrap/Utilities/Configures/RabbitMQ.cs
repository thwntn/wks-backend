namespace Wks.Mains;

///
/// Summary:
///     Provides RabbitMQ MassTransit integration extension methods.
///     Configures message bus transport and optional consumers based on runtime settings.
///
public static class MassTransitExtensions
{
    private static readonly bool IsActive =
        Runtime
            .Mode.Env()
            .Equals(
                nameof(SystemModeEnum.Development),
                StringComparison.OrdinalIgnoreCase
            ) || Runtime.WithConsumer.Env().SafeBool();

    ///
    /// Summary:
    ///     Adds MassTransit with RabbitMQ transport to the service collection.
    ///     Optionally registers consumers when RabbitMQ is active in the current runtime.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection to register MassTransit services on.
    ///   configureConsumers:
    ///     Action to configure consumer registration if RabbitMQ is active.
    ///   host:
    ///     The RabbitMQ host URL.
    ///
    /// Returns:
    ///     The updated IServiceCollection with MassTransit configured.
    ///
    public static IServiceCollection AddRabbitMqMassTransit(
        this IServiceCollection services,
        Action<IBusRegistrationConfigurator> configureConsumers,
        string host
    )
    {
        services.AddMassTransit(setting =>
        {
            /// Summary:
            ///   Add consumer to RabbitMQ, each consumer will listen to a specific queue and process messages from that queue
            if (IsActive)
                configureConsumers(setting);

            /// Summary:
            ///   Configure RabbitMQ as the transport mechanism for MassTransit.
            ///   This involves setting the host address and ensuring that all registered consumers are properly connected to their respective queues.
            setting.UsingRabbitMq(
                (context, config) =>
                {
                    config.Host(host);
                    config.ConfigurePublish(pipe => pipe.UseFilter(context.GetRequiredService<RabbitMQPublishLoggingFilter>()));
                    config.ConfigureSend(pipe => pipe.UseFilter(context.GetRequiredService<RabbitMQSendLoggingFilter>()));
                    config.UseFilter(context.GetRequiredService<RabbitMQConsumeLoggingFilter>());
                    config.ConfigureEndpoints(context);
                    config.ConfigureJsonSerializerOptions(options =>
                    {
                        options.ReferenceHandler = System
                            .Text
                            .Json
                            .Serialization
                            .ReferenceHandler
                            .IgnoreCycles;

                        return options;
                    });
                }
            );

            /// Summary:
            ///   Formats MassTransit endpoint/queue names in kebab-case
            ///   (e.g. SendEmailConsumer -> send-email).
            ///   The false argument disables including the namespace in the generated name.
            setting.SetEndpointNameFormatter(
                new KebabCaseEndpointNameFormatter(string.Empty, false)
            );
        });

        /// Summary:
        ///     Register after MassTransit so queued messages drain before the bus stops.
        services.AddSingleton<RabbitMQPublishLoggingFilter>();
        services.AddSingleton<RabbitMQSendLoggingFilter>();
        services.AddSingleton<RabbitMQConsumeLoggingFilter>();
        services.AddSingleton<QueuePublisher>();
        services.AddSingleton<IQueuePublisher>(provider => provider.GetRequiredService<QueuePublisher>());
        services.AddHostedService(provider => provider.GetRequiredService<QueuePublisher>());

        return services;
    }
}
