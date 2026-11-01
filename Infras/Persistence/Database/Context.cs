namespace Wks.Databases;
///
/// Summary:
///     Entity Framework context for the retained WKS platform entities.
///     Uses the supplied context options to access the configured PostgreSQL database.
///
public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    ///
    /// Summary:
    ///     Configures the platform model and seeds the DEFAULT_PERMISSION setting with an empty list.
    ///     The seed identifier and timestamp are fixed so migration snapshots remain stable.
    ///
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Setting>().HasData(new
        {
            Id = Guid.Parse("5c2a98b3-3c07-4dd1-90d1-74d08b731251"),
            Name = nameof(SettingNames.DEFAULT_PERMISSION),
            Value = "[]",
            CreatedAt = new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc),
        });
    }
    ///
    /// Summary:
    ///     Registered object-storage artifacts and download tokens.
    ///
    public DbSet<Artifact> Artifact { get; set; }
    ///
    /// Summary:
    ///     Login accounts and their authentication state.
    ///
    public DbSet<Account> Account { get; set; }
    ///
    /// Summary:
    ///     Account roles used to group permission codes.
    ///
    public DbSet<Role> Role { get; set; }
    ///
    /// Summary:
    ///     Permission codes assigned to account roles.
    ///
    public DbSet<Permission> RolePermission { get; set; }
    ///
    /// Summary:
    ///     Recorded account login sessions.
    ///
    public DbSet<LoginAccount> LoginAccount { get; set; }
    ///
    /// Summary:
    ///     Account profile and contact information.
    ///
    public DbSet<Profile> Profile { get; set; }
    ///
    /// Summary:
    ///     Account audit actions.
    ///
    public DbSet<Action> Action { get; set; }
    ///
    /// Summary:
    ///     Notifications belonging to profiles.
    ///
    public DbSet<Notification> Notification { get; set; }
    ///
    /// Summary:
    ///     Google authentication identities linked to accounts.
    ///
    public DbSet<Google> Google { get; set; }
    ///
    /// Summary:
    ///     Feedback and support conversations.
    ///
    public DbSet<Feedback> Feedback { get; set; }
    ///
    /// Summary:
    ///     Messages within feedback conversations.
    ///
    public DbSet<FeedbackMessage> FeedbackMessage { get; set; }
    ///
    /// Summary:
    ///     System settings, including the default account permission list.
    ///
    public DbSet<Setting> Setting { get; set; }
    ///
    /// Summary:
    ///     Icons available to profiles.
    ///
    public DbSet<Icon> Icon { get; set; }
    ///
    /// Summary:
    ///     Domains associated with profiles.
    ///
    public DbSet<Domain> Domain { get; set; }
    ///
    /// Summary:
    ///     Application release versions.
    ///
    public DbSet<AppVersion> AppVersion { get; set; }
    ///
    /// Summary:
    ///     Changelog features associated with application versions.
    ///
    public DbSet<AppFeature> AppFeature { get; set; }
}
