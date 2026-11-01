namespace Wks.Objects;

///
/// Summary:
///     Defines query options for pagination, searching, and sorting operations.
///
public class QueryOptions
{
    public const string PREFIX_SPLIT = ",";
    public const string PREFIX_CONCAT = "_";

    ///
    /// Summary:
    ///     Gets or sets the maximum number of records to return in a single page.
    ///
    /// Returns:
    ///     The maximum number of records to return in a single page.
    ///
    [NotMapped]
    public int Limit { get; set; } = 0;

    ///
    /// Summary:
    ///     Gets or sets the page number for pagination.
    ///
    /// Returns:
    ///     The page number for pagination.
    ///
    [NotMapped]
    public int Page { get; set; } = 0;

    ///
    /// Summary:
    ///     Gets or sets the comma-separated list of column names to search in.
    ///
    /// Returns:
    ///     The comma-separated list of column names to search in.
    ///
    [NotMapped]
    public string SearchColumns { get; set; } = string.Empty;

    ///
    /// Summary:
    ///     Gets or sets the search text to filter records.
    ///
    /// Returns:
    ///     The search text to filter records.
    ///
    [NotMapped]
    public string SearchText { get; set; } = string.Empty;

    ///
    /// Summary:
    ///     Gets or sets the column name to sort the results by.
    ///
    /// Returns:
    ///     The column name to sort the results by.
    ///
    [NotMapped]
    public string SortColumn { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the sort order for the results.
    ///
    /// Returns:
    ///     The sort order for the results.
    ///
    [NotMapped]
    public SortOrder SortOrder { get; set; } = SortOrder.DESC;
}
