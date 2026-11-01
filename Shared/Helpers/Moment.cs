namespace Wks.Mains;

///
/// Summary:
///     Provides date and time utilities including timezone handling, date range calculations,
///     and time formatting operations. Supports both UTC and business timezone configurations.
///
public class Moment
{
    ///
    /// Summary:
    ///     Date format string for displaying dates without time component.
    ///     Format: YYYY-MM-DD
    ///
    public const string FORMAT_DATE_WITHOUT_TIME = "yyyy-MM-dd";

    ///
    /// Summary:
    ///     Date format string for displaying year and month only.
    ///     Format: YYYY-MM
    ///
    public const string FORMAT_YEAR_MONTH = "yyyy-MM";

    ///
    /// Summary:
    ///     Date format string for displaying month and year.
    ///     Format: MM/YYYY
    ///
    public const string FORMAT_MONTH_YEAR = "MM/yyyy";

    ///
    /// Summary:
    ///     ISO 8601 format string for date and time serialization.
    ///     Format: YYYY-MM-DDTHH:MM:SS.FFFFFFFZ
    ///
    public const string FORMAT_ISO_STRING = "yyyy-MM-ddTHH:mm:ss.fffffffZ";

    ///
    /// Summary:
    ///     Number of days in a week for date calculations.
    ///
    public const int WEEK = 7;

    ///
    /// Summary:
    ///     Retrieves a list of all available timezone IDs from the system.
    ///     Useful for timezone selection in user interfaces or configuration.
    ///
    /// Returns:
    ///     An enumerable collection of timezone information objects available on the system.
    ///
    public static List<TimeZoneInfo> ListTimeZoneId()
    {
        /// Logic:
        ///     Gets all system timezones and returns them as a list.
        ///
        List<TimeZoneInfo> systemTimeZones = TimeZoneInfo
            .GetSystemTimeZones()
            .Select(timeZoneInfo => timeZoneInfo)
            .ToList();
        return systemTimeZones;
    }

    ///
    /// Summary:
    ///     UTC timer instance for working with universal coordinated time.
    ///
    public static readonly Timer Utc = new(nameof(Utc));

    ///
    /// Summary:
    ///     Business timer instance configured with the application's timezone.
    ///
    public static readonly Timer Business = new(Runtime.Timezone.Env());
}

///
/// Summary:
///     Represents a timer that handles timezone conversions and date range calculations.
///     Configured with a specific timezone for consistent time handling across the application.
///
public class Timer(string timeZone)
{
    ///
    /// Summary:
    ///     Timezone information for the timer, loaded from the provided timezone identifier.
    ///     Used for converting between UTC and local time.
    ///
    private readonly TimeZoneInfo _timeZoneInfo =
        TimeZoneInfo.FindSystemTimeZoneById(timeZone);

    ///
    /// Summary:
    ///     Gets the current date and time converted to the configured timezone.
    ///     Uses the timezone specified in initialization for consistent time handling.
    ///
    /// Returns:
    ///     Current DateTime converted to the configured timezone.
    ///
    public DateTime Now
    {
        get { return TimeZoneInfo.ConvertTime(DateTime.Now, _timeZoneInfo); }
    }

    ///
    /// Summary:
    ///     Calculates the date range for a given month, returning the first and last day.
    ///     Useful for generating monthly reports or date range queries.
    ///
    /// Parameters:
    ///   dateTime:
    ///     The DateTime object representing any date within the target month.
    ///
    /// Returns:
    ///     A tuple containing the first day (DateTime) and last day (DateTime) of the month.
    ///
    public (DateTime, DateTime) RangeTimeMonth(DateTime dateTime)
    {
        /// Logic:
        ///     Creates the first day of the month by setting day to 1.
        ///
        int prefix = 1;
        DateTime minDate = new(dateTime.Year, dateTime.Month, prefix);

        /// Logic:
        ///     Creates the last day of the month by adding 1 month and subtracting 1 day.
        ///
        DateTime maxDate = new DateTime(
            dateTime.Year,
            dateTime.Month + prefix,
            prefix
        ).AddDays(-prefix);
        return (minDate, maxDate);
    }
}
