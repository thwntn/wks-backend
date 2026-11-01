namespace Wks.Mains;

///
/// Summary:
///     Configures Entity Framework Core with PostgreSQL database provider.
///     Feature: (DbContext registration with query optimization and logging)
///
/// -> Provides database access layer with optimized query performance,
///     no-tracking behavior, and query splitting for complex queries.
///
public static class DatabaseExtension
{
    ///
    /// Summary:
    ///     Registers DatabaseContext with PostgreSQL provider and performance optimizations.
    ///     Feature: (EF Core with NoTracking, QuerySplitting, and error logging)
    ///
    /// -> Configures database connection with optimized settings for read-heavy
    ///     operations and prevents N+1 query problems through query splitting.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register database services.
    ///
    public static void DatabaseConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Add Entity Framework Core DbContext with PostgreSQL provider
        ///     Feature: (DatabaseContext with performance and compatibility settings)
        ///
        /// -> Enables legacy timestamp behavior, disables change tracking,
        ///     splits complex queries, and logs errors to console.
        ///
        services.AddDbContext<DatabaseContext>(
            options =>
            {
                ///
                /// Summary:
                ///     Enable legacy timestamp behavior for PostgreSQL compatibility
                ///     Feature: (Timestamp handling configuration via AppContext switch)
                ///
                /// -> Ensures consistent DateTime behavior across different
                ///     PostgreSQL versions and .NET runtime environments.
                ///
                AppContext.SetSwitch(Runtime.EnableLegacyTimestampBehavior.Env(), true);

                ///
                /// Summary:
                ///     Disable query tracking for improved read performance
                ///     Feature: (NoTracking behavior to prevent entity state management overhead)
                ///
                /// -> Reduces memory usage and improves query speed for read-only
                ///     operations by skipping change tracking mechanism.
                ///
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                ///
                /// Summary:
                ///     Configure PostgreSQL connection with query splitting
                ///     Feature: (Npgsql provider with SplitQuery for collection navigation)
                ///
                /// -> Prevents cartesian explosion in queries with multiple collections
                ///     by splitting into separate SQL queries automatically.
                ///
                options.UseNpgsql(
                    Runtime.Database.Env(),
                    options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                );

                ///
                /// Summary:
                ///     Configure error-level logging for database commands
                ///     Feature: (Console logging for database operations and errors)
                ///
                /// -> Outputs SQL commands and errors to console for debugging
                ///     while filtering out verbose information messages.
                ///
                options.LogTo(
                    Console.WriteLine,
                    [DbLoggerCategory.Database.Command.Name],
                    LogLevel.Error
                );
            },
            ServiceLifetime.Scoped
        );
    }
}
