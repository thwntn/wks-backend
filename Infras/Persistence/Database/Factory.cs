using Microsoft.EntityFrameworkCore.Design;

namespace Wks.Databases;

///
/// Summary:
///     Creates the EF context for design-time migration commands without starting the web host or workers.
///
public sealed class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    ///
    /// Summary:
    ///     Loads optional local environment settings and creates a PostgreSQL context.
    ///     Uses the Database environment variable, or a local development connection when it is absent.
    ///     The args parameter contains EF tool arguments; this factory does not use them.
    ///
    public DatabaseContext CreateDbContext(string[] args)
    {
        Reader.ConfigureAsync().GetAwaiter().GetResult();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql(Runtime.Database.Env() ?? "Host=localhost;Port=5432;Database=wks;Username=wks;Password=change-me")
            .Options;
        return new DatabaseContext(options);
    }
}
