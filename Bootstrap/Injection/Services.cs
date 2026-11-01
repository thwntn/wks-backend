namespace Wks.Mains;
///
/// Summary:
///     Registers application services, handlers, adapters and infrastructure services for the WKS base.
///
public class Services
{
    ///
    /// Summary:
    ///     Registers the retained platform dependencies with their scoped or singleton lifetimes.
    ///     The services argument is the application service collection configured during startup.
    ///
    public static void Injection(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGetFile, GetFile>();
        services.AddScoped<IPresignedUploadFile, PresignedUploadFile>();
        services.AddScoped<IUploadFile, UploadFile>();
        services.AddScoped<IGetArtifact, GetArtifact>();
        services.AddScoped<ICreateArtifact, CreateArtifact>();
        services.AddScoped<IStaticArtifact, StaticArtifact>();
        services.AddScoped<IDataStreamArtifact, DataStreamArtifact>();
        services.AddScoped<ICreateArtifactHandler, CreateArtifactHandler>();
        services.AddScoped<IPresignedUploadArtifact, PresignedUploadArtifact>();
        services.AddScoped<IRequest, RequestService>();
        services.AddSingleton<ICache, Cache>();
        services.AddScoped<ILoginAuth, LoginAuth>();
        services.AddScoped<ISignupAuth, SignupAuth>();
        services.AddScoped<IResetPasswordAuth, ResetPasswordAuth>();
        services.AddScoped<IVerifyAuth, VerifyAuth>();
        services.AddScoped<IConfirmCodeAuth, ConfirmCodeAuth>();
        services.AddScoped<IValidateAccountGoogle, ValidateAccountGoogle>();
        services.AddScoped<IGetUrlGoogleAuth, GetUrlGoogleAuth>();
        services.AddScoped<IStatusSetupGoogleAuth, StatusSetupGoogleAuth>();
        services.AddScoped< IGenerateAccessTokenAuthPort, GenerateAccessTokenAuthAdapter >();
        services.AddScoped<ISetupAccountAuth, SetupAccountAuth>();
        services.AddScoped<ISetupAccountAuthHandler, SetupAccountAuthHandler>();
        services.AddScoped< ISetupAccountAuthPublisher, SetupAccountAuthPublisher >();
        services.AddScoped<ISignupAuthHandler, SignupAuthHandler>();
        services.AddScoped<IUsePasswordAuthHandler, UsePasswordAuthHandler>();
        services.AddScoped<IConfirmCodeAuthHandler, ConfirmCodeAuthHandler>();
        services.AddScoped< IResetPasswordAuthHandler, ResetPasswordAuthHandler >();
        services.AddScoped< IChangePasswordAuthHandler, ChangePasswordAuthHandler >();
        services.AddScoped<ILoginGoogleAuthHandler, LoginGoogleAuthHandler>();
        services.AddScoped<ISessionTokenInformation, SessionTokenInformation>();
        services.AddScoped<IGetAccount, GetAccount>();
        services.AddScoped<IGetGroupToken, GetGroupToken>();
        services.AddScoped<ISetupCookie, SetupCookie>();
        services.AddScoped<IParseToken, ParseToken>();
        services.AddScoped<IGetRefreshToken, GetRefreshToken>();
        services.AddScoped<IGenerateAccessToken, GenerateJsonWebToken>();
        services.AddScoped<IGenerateRefreshToken, GenerateRefreshToken>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
        services.AddScoped<IResolveSessionToken, ResolveSessionToken>();
        services.AddScoped<IResolveIdentityToken, ResolveIdentityToken>();
        services.AddScoped< IGenerateSessionTokenHandler, GenerateSessionTokenHandler >();
        services.AddScoped< ICreateJsonWebTokenHandler, CreateJsonWebTokenHandler >();
        services.AddScoped<IListProfile, ListProfile>();
        services.AddScoped<IMeProfile, MeProfile>();
        services.AddScoped<IGetProfile, GetProfile>();
        services.AddScoped<IUpdateProfile, UpdateProfile>();
        services.AddScoped<IChangeAvatarProfile, ChangeAvatarProfile>();
        services.AddScoped< IChangeCoverPictureProfile, ChangeCoverPictureProfile >();
        services.AddScoped< IGetAccountWithRoleProfile, GetAccountWithRoleProfile >();
        services.AddScoped<IUpdateProfileHandler, UpdateProfileHandler>();
        services.AddScoped<IChangeAvatarHandler, ChangeAvatarHandler>();
        services.AddScoped< IChangeCoverPictureHandler, ChangeCoverPictureHandler >();
        services.AddScoped<IListDomain, ListDomain>();
        services.AddScoped<IGetDomain, GetDomain>();
        services.AddScoped<ITypeDomain, TypeDomain>();
        services.AddScoped<IResolveDomain, ResolveDomain>();
        services.AddScoped<ICreateDomain, CreateDomain>();
        services.AddScoped<ICreateDomainHandler, CreateDomainHandler>();
        services.AddScoped<IUpdateDomain, UpdateDomain>();
        services.AddScoped<IUpdateDomainHandler, UpdateDomainHandler>();
        services.AddScoped<IRemoveDomain, RemoveDomain>();
        services.AddScoped<IRemoveDomainHandler, RemoveDomainHandler>();
        services.AddScoped<ICreateAction, CreateAction>();
        services.AddScoped<ICreateActionHandler, CreateActionHandler>();
        services.AddScoped<ICreateActionPublisher, CreateActionPublisher>();
        services.AddScoped<IListAction, ListAction>();
        services.AddScoped<ICreateActor, CreateActor>();
        services.AddScoped<IListActor, ListActor>();
        services.AddScoped<IUpdateActor, UpdateActor>();
        services.AddScoped<IStatusActor, StatusActor>();
        services.AddScoped<IListRoleActor, ListRoleActor>();
        services.AddScoped<ICreateRoleActor, CreateRoleActor>();
        services.AddScoped<IUpdateRoleActor, UpdateRoleActor>();
        services.AddScoped<IRemoveRoleActor, RemoveRoleActor>();
        services.AddScoped<IPermissionActor, PermissionActor>();
        services.AddScoped<IRoleActorPort, RoleActorAdapter>();
        services.AddScoped<ICreateActorHandler, CreateActorHandler>();
        services.AddScoped<IUpdateActorHandler, UpdateActorHandler>();
        services.AddScoped<IStatusActorHandler, StatusActorHandler>();
        services.AddScoped<ICreateRoleActorHandler, CreateRoleActorHandler>();
        services.AddScoped<IUpdateRoleActorHandler, UpdateRoleActorHandler>();
        services.AddScoped<IRemoveRoleActorHandler, RemoveRoleActorHandler>();
        services.AddScoped<IListProfileAdminPort, ListProfileAdminAdapter>();
        services.AddScoped<IGetProfileAdminPort, GetProfileAdminAdapter>();
        services.AddScoped<ICreateRoleAdminPort, CreateRoleAdminAdapter>();
        services.AddScoped<ICreateRoleAdminHandler, CreateRoleAdminHandler>();
        services.AddScoped<IListRolesAdminPort, ListRolesAdminAdapter>();
        services.AddScoped<IGetRoleAdminPort, GetRoleAdminAdapter>();
        services.AddScoped<IUpdateRoleAdminPort, UpdateRoleAdminAdapter>();
        services.AddScoped<IUpdateRoleAdminHandler, UpdateRoleAdminHandler>();
        services.AddScoped<IRemoveRoleAdminPort, RemoveRoleAdminAdapter>();
        services.AddScoped<IRemoveRoleAdminHandler, RemoveRoleAdminHandler>();
        services.AddScoped<IAssignRoleAdminPort, AssignRoleAdminAdapter>();
        services.AddScoped<IAssignRoleAdminHandler, AssignRoleAdminHandler>();
        services.AddScoped<IUnassignRoleAdminPort, UnassignRoleAdminAdapter>();
        services.AddScoped< IUnassignRoleAdminHandler, UnassignRoleAdminHandler >();
        services.AddScoped< IPermissionRoleAdminPort, PermissionRoleAdminAdapter >();
        services.AddScoped< ICreateVersionAdminPort, CreateVersionAdminAdapter >();
        services.AddScoped< ICreateVersionAdminHandler, CreateVersionAdminHandler >();
        services.AddScoped< IUpdateVersionAdminPort, UpdateVersionAdminAdapter >();
        services.AddScoped< IUpdateVersionAdminHandler, UpdateVersionAdminHandler >();
        services.AddScoped< IRemoveVersionAdminPort, RemoveVersionAdminAdapter >();
        services.AddScoped< IRemoveVersionAdminHandler, RemoveVersionAdminHandler >();
        services.AddScoped< ICreateFeatureVersionAdminPort, CreateFeatureVersionAdminAdapter >();
        services.AddScoped< ICreateFeatureVersionAdminHandler, CreateFeatureVersionAdminHandler >();
        services.AddScoped< IRemoveFeatureVersionAdminPort, RemoveFeatureVersionAdminAdapter >();
        services.AddScoped< IRemoveFeatureVersionAdminHandler, RemoveFeatureVersionAdminHandler >();
        services.AddScoped<INameSettingAdminPort, NameSettingAdminAdapter>();
        services.AddScoped<IListSettingAdminPort, ListSettingAdminAdapter>();
        services.AddScoped< IEnsureAdminSettingAdminPort, EnsureAdminSettingAdminAdapter >();
        services.AddScoped< ICreateSettingAdminPort, CreateSettingAdminAdapter >();
        services.AddScoped< ICreateSettingAdminHandler, CreateSettingAdminHandler >();
        services.AddScoped< IUpdateSettingAdminPort, UpdateSettingAdminAdapter >();
        services.AddScoped< IUpdateSettingAdminHandler, UpdateSettingAdminHandler >();
        services.AddScoped< IRemoveSettingAdminPort, RemoveSettingAdminAdapter >();
        services.AddScoped< IRemoveSettingAdminHandler, RemoveSettingAdminHandler >();
        services.AddScoped<ITypeDomainAdminPort, TypeDomainAdminAdapter>();
        services.AddScoped<IUpdateDomainAdminPort, UpdateDomainAdminAdapter>();
        services.AddScoped< IUpdateDomainAdminHandler, UpdateDomainAdminHandler >();
        services.AddScoped<IRemoveDomainAdminPort, RemoveDomainAdminAdapter>();
        services.AddScoped< IRemoveDomainAdminHandler, RemoveDomainAdminHandler >();
        services.AddScoped<IDomainAdminPort, DomainAdminAdapter>();
        services.AddScoped<ISetupDomainAdmin, SetupDomainAdmin>();
        services.AddScoped<ISetupDomainAdminHandler, SetupDomainAdminHandler>();
        services.AddScoped<IClearCacheAdmin, ClearCacheAdmin>();
        services.AddScoped<ISyncFeaturesAdmin, SyncFeaturesAdmin>();
        services.AddScoped< ISyncFeaturesAdminHandler, SyncFeaturesAdminHandler >();
        services.AddScoped<IPermissionCachePort, PermissionCacheAdapter>();
        services.AddScoped<ICreateRole, CreateRole>();
        services.AddScoped<IListRoles, ListRoles>();
        services.AddScoped<IGetRole, GetRole>();
        services.AddScoped<IUpdateRole, UpdateRole>();
        services.AddScoped<IRemoveRole, RemoveRole>();
        services.AddScoped<IPermissionRole, PermissionRole>();
        services.AddScoped<ICreateRoleHandler, CreateRoleHandler>();
        services.AddScoped<IUpdateRoleHandler, UpdateRoleHandler>();
        services.AddScoped<IRemoveRoleHandler, RemoveRoleHandler>();
        services.AddScoped<IMakeAdmin, MakeAdmin>();
        services.AddScoped<IMakeAdminHandler, MakeAdminHandler>();
        services.AddScoped<IAssignRole, AssignRole>();
        services.AddScoped<IUnassignRole, UnassignRole>();
        services.AddScoped<IAssignRoleHandler, AssignRoleHandler>();
        services.AddScoped<IUnassignRoleHandler, UnassignRoleHandler>();
        services.AddScoped<ICheckPermission, CheckPermission>();
        services.AddScoped<ICreateVersion, CreateVersion>();
        services.AddScoped<ICreateVersionHandler, CreateVersionHandler>();
        services.AddScoped<IGetVersion, GetVersion>();
        services.AddScoped<ICurrentVersion, CurrentVersion>();
        services.AddScoped<IListVersion, ListVersion>();
        services.AddScoped<IUpdateVersion, UpdateVersion>();
        services.AddScoped<IUpdateVersionHandler, UpdateVersionHandler>();
        services.AddScoped<IRemoveVersion, RemoveVersion>();
        services.AddScoped<IRemoveVersionHandler, RemoveVersionHandler>();
        services.AddScoped<ICreateFeatureVersion, CreateFeatureVersion>();
        services.AddScoped< ICreateFeatureVersionHandler, CreateFeatureVersionHandler >();
        services.AddScoped<IRemoveFeatureVersion, RemoveFeatureVersion>();
        services.AddScoped< IRemoveFeatureVersionHandler, RemoveFeatureVersionHandler >();
        services.AddScoped<ICreateFeedback, CreateFeedback>();
        services.AddScoped<ICreateFeedbackHandler, CreateFeedbackHandler>();
        services.AddScoped<IReplyFeedback, ReplyFeedback>();
        services.AddScoped<IReplyFeedbackHandler, ReplyFeedbackHandler>();
        services.AddScoped<IListFeedback, ListFeedback>();
        services.AddScoped<IListStatusFeedback, ListStatusFeedback>();
        services.AddScoped<IPinFeedback, PinFeedback>();
        services.AddScoped<IPinFeedbackHandler, PinFeedbackHandler>();
        services.AddScoped<IUnpinFeedback, UnpinFeedback>();
        services.AddScoped<IUnpinFeedbackHandler, UnpinFeedbackHandler>();
        services.AddScoped<IListFeature, ListFeature>();
        services.AddScoped<IAddFeature, AddFeature>();
        services.AddScoped<IRemoveFeature, RemoveFeature>();
        services.AddScoped<IPublishFeature, PublishFeature>();
        services.AddScoped<IPublishFeatureHandler, PublishFeatureHandler>();
        services.AddScoped<IAddFeatureHandler, AddFeatureHandler>();
        services.AddScoped<IRemoveFeatureHandler, RemoveFeatureHandler>();
        services.AddScoped<ISystemPushNotification, SystemPushNotification>();
        services.AddScoped<ICreateNotification, CreateNotification>();
        services.AddScoped< ICreateNotificationHandler, CreateNotificationHandler >();
        services.AddScoped<IHandleNotification, HandleNotification>();
        services.AddScoped<IListNotification, ListNotification>();
        services.AddScoped<IReadNotification, ReadNotification>();
        services.AddScoped<IReadNotificationHandler, ReadNotificationHandler>();
        services.AddScoped< IHandleNotificationHandler, HandleNotificationHandler >();
        services.AddScoped<IGetSetting, GetSetting>();
        services.AddScoped<IListSetting, ListSetting>();
        services.AddScoped<INameSetting, NameSetting>();
        services.AddScoped<ICreateSetting, CreateSetting>();
        services.AddScoped<IUpdateSetting, UpdateSetting>();
        services.AddScoped<IRemoveSetting, RemoveSetting>();
        services.AddScoped<IEnsureAdminSetting, EnsureAdminSetting>();
        services.AddScoped<ICreateSettingHandler, CreateSettingHandler>();
        services.AddScoped<IUpdateSettingHandler, UpdateSettingHandler>();
        services.AddScoped<IRemoveSettingHandler, RemoveSettingHandler>();
        services.AddScoped<IListIcon, ListIcon>();
        services.AddScoped<ICreateIcon, CreateIcon>();
        services.AddScoped<IRemoveIcon, RemoveIcon>();
        services.AddScoped<ICreateIconHandler, CreateIconHandler>();
        services.AddScoped<IRemoveIconHandler, RemoveIconHandler>();
        services.AddScoped<ISendMail, SendMail>();
        services.AddScoped<ISendCodeAuth, SendCodeAuth>();
        services.AddScoped<IGetAccessTokenGoogle, GetAccessTokenGoogle>();
        services.AddScoped<IGetProfileGoogle, GetProfileGoogle>();
        services.AddScoped<IGetUrlGoogle, GetUrlGoogle>();
        services.AddScoped<ISendFcm, SendFcm>();
        ///
        /// Summary:
        ///     Registers Financial and its shared hashtag dependencies.
        ///
        services.AddScoped<IOverviewDashboard, OverviewDashboard>();
        services.AddScoped<IGetExpenseAnalyst, GetExpenseAnalyst>();
        services.AddScoped<IGetTopTransaction, GetTopTransaction>();
        services.AddScoped<IGetFinancialReport, GetFinancialReport>();
        services.AddScoped<
            IGetActivitiesFinancialAnalytics,
            GetActivityFinancialAnalytics
        >();
        services.AddScoped<
            IGroupExpenseFinancialAnalytics,
            GroupExpenseFinancialAnalytics
        >();
        services.AddScoped<IListExpense, ListExpense>();
        services.AddScoped<ICreateExpense, CreateExpense>();
        services.AddScoped<IUpdateExpense, UpdateExpense>();
        services.AddScoped<IRemoveExpense, RemoveExpense>();
        services.AddScoped<IInfoExpense, InfoExpense>();
        services.AddScoped<IFilterExpense, FilterExpense>();
        services.AddScoped<IBannerExpense, BannerExpense>();
        services.AddScoped<ICalculatedExpense, CalculatedExpense>();
        services.AddScoped<IGetCounterExpense, GetCounterExpense>();
        services.AddScoped<IPinExpense, PinExpense>();
        services.AddScoped<IUnpinExpense, UnpinExpense>();
        services.AddScoped<ICreateExpenseHandler, CreateExpenseHandler>();
        services.AddScoped<IUpdateExpenseHandler, UpdateExpenseHandler>();
        services.AddScoped<IRemoveExpenseHandler, RemoveExpenseHandler>();
        services.AddScoped<IPinExpenseHandler, PinExpenseHandler>();
        services.AddScoped<IUnpinExpenseHandler, UnpinExpenseHandler>();
        services.AddScoped<IRealtimeExpensePort, RealtimeExpenseAdapter>();
        services.AddScoped<IListTimeline, ListTimeline>();
        services.AddScoped<ICreateTimeline, CreateTimeline>();
        services.AddScoped<IUpdateTimeline, UpdateTimeline>();
        services.AddScoped<IRemoveTimeline, RemoveTimeline>();
        services.AddScoped<IInfoTimeline, InfoTimeline>();
        services.AddScoped<ICounterTimeline, CounterTimeline>();
        services.AddScoped<ITimeline, TimelineUseCase>();
        services.AddScoped<ICloneTimeline, CloneTimeline>();
        services.AddScoped<IUpdateTagTimeline, UpdateTagTimeline>();
        services.AddScoped<IPinTimeline, PinTimeline>();
        services.AddScoped<IUnpinTimeline, UnpinTimeline>();
        services.AddScoped<IChartTimeline, ChartTimeline>();
        services.AddScoped<IRecentTimeline, RecentTimeline>();
        services.AddScoped<IPeriodTimeline, PeriodTimeline>();
        services.AddScoped<ICashflowTimeline, CashflowTimeline>();
        services.AddScoped<ICreateTimelineHandler, CreateTimelineHandler>();
        services.AddScoped<IUpdateTimelineHandler, UpdateTimelineHandler>();
        services.AddScoped<IRemoveTimelineHandler, RemoveTimelineHandler>();
        services.AddScoped<ICloneTimelineHandler, CloneTimelineHandler>();
        services.AddScoped<
            IUpdateTagTimelineHandler,
            UpdateTagTimelineHandler
        >();
        services.AddScoped<IPinTimelineHandler, PinTimelineHandler>();
        services.AddScoped<IUnpinTimelineHandler, UnpinTimelineHandler>();
        services.AddScoped<IRealtimeTimelinePort, RealtimeTimelineAdapter>();
        services.AddScoped<IListExpenseTransaction, ListExpenseTransaction>();
        services.AddScoped<
            ICreateExpenseTransaction,
            CreateExpenseTransaction
        >();
        services.AddScoped<
            IRemoveExpenseTransaction,
            RemoveExpenseTransaction
        >();
        services.AddScoped<
            IUpdateExpenseTransaction,
            UpdateExpenseTransaction
        >();
        services.AddScoped<
            IWithCategoryExpenseTransaction,
            WithCategoryExpenseTransaction
        >();
        services.AddScoped<
            ICounterExpenseTransaction,
            CounterExpenseTransaction
        >();
        services.AddScoped<
            IStatisticExpenseTransaction,
            StatisticExpenseTransaction
        >();
        services.AddScoped<
            IExpenditureExpenseTransaction,
            ExpenditureExpenseTransaction
        >();
        services.AddScoped<ISuggestExpense, SuggestExpense>();
        services.AddScoped<
            ICreateExpenseTransactionHandler,
            CreateExpenseTransactionHandler
        >();
        services.AddScoped<
            IUpdateExpenseTransactionHandler,
            UpdateExpenseTransactionHandler
        >();
        services.AddScoped<
            IRemoveExpenseTransactionHandler,
            RemoveExpenseTransactionHandler
        >();
        services.AddScoped<
            IRealtimeTransactionPort,
            RealtimeTransactionAdapter
        >();
        services.AddScoped<ICreateCash, CreateCash>();
        services.AddScoped<IRemoveCash, RemoveCash>();
        services.AddScoped<IListCashDescription, ListCashDescription>();
        services.AddScoped<IRecentCashDescription, RecentCashDescription>();
        services.AddScoped<ICreateCashDescription, CreateCashDescription>();
        services.AddScoped<ICreateCashHandler, CreateCashHandler>();
        services.AddScoped<IRemoveCashHandler, RemoveCashHandler>();
        services.AddScoped<
            ICreateCashDescriptionHandler,
            CreateCashDescriptionHandler
        >();
        services.AddScoped<IListHashTag, ListHashTag>();
        services.AddScoped<IGetHashTag, GetHashTag>();
        services.AddScoped<ICreateHashTag, CreateHashTag>();
        services.AddScoped<IUpdateHashTag, UpdateHashTag>();
        services.AddScoped<IRemoveHashTag, RemoveHashTag>();
        services.AddScoped<ICreateHashTagHandler, CreateHashTagHandler>();
        services.AddScoped<IUpdateHashTagHandler, UpdateHashTagHandler>();
        services.AddScoped<ICounterHomeFinancial, CounterHomeFinancial>();
        services.AddScoped<
            IMonthlyChartHomeFinancial,
            MonthlyChartHomeFinancial
        >();
        services.AddScoped<
            ISpendingByExpenseHomeFinancial,
            SpendingByExpenseHomeFinancial
        >();
        services.AddScoped<
            IRecentTransactionHomeFinancial,
            RecentTransactionHomeFinancial
        >();

        /// Summary: Shared text provider and Financial description template generation.
        services.AddScoped<IGenerateTextIntelligent, GenerateTextIntelligent>();
        services.AddScoped<ITemplateIntelligence, TemplateIntelligence>();
        services.AddScoped<ITemplateGenerateIntelligence, TemplateGenerateIntelligence>();

    }
}
