namespace Wks.Mains;
///
/// Summary:
///     Registers persistence repositories for the retained WKS platform entities.
///
public class Repositories
{
    ///
    /// Summary:
    ///     Binds repository interfaces to their scoped Entity Framework implementations.
    ///     The services argument is the application service collection configured during startup.
    ///
    public static void Injection(IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ILoginAccountRepository, LoginAccountRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IActionRepository, ActionRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IDomainRepository, DomainRepository>();
        services.AddScoped<IArtifactRepository, ArtifactRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped< IFeedbackMessageRepository, FeedbackMessageRepository >();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IIconRepository, IconRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IVersionRepository, VersionRepository>();
        services.AddScoped<IFeatureRepository, FeatureVersionRepository>();
        ///
        /// Summary:
        ///     Registers Financial and its shared hashtag dependencies.
        ///
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<ITimelineRepository, TimelineRepository>();
        services.AddScoped<
            IExpenseTransactionRepository,
            ExpenseTransactionRepository
        >();
        services.AddScoped<IHashTagRepository, TagRepository>();
        services.AddScoped<ICashRepository, CashRepository>();
        services.AddScoped<
            ICashDescriptionRepository,
            CashDescriptionRepository
        >();

    }
}
