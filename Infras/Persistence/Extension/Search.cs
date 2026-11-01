namespace Wks.Mains;

///
/// Summary:
///     Extension methods for Entity Framework Core query operations.
///     Provides dynamic search and ordering capabilities for queryable collections.
///
public static class EFCoreExtension
{
    ///
    /// Summary:
    ///     Performs a text search across multiple properties of entities.
    ///     Creates a dynamic query to search for text in specified properties.
    ///
    /// Parameters:
    ///     source:
    ///         The source queryable collection to search within.
    ///     text:
    ///         The text to search for in the specified properties.
    ///     properties:
    ///         Array of property names to search within.
    ///
    /// Returns:
    ///     IQueryable collection filtered by the search criteria, or original source if error occurs.
    ///
    public static IQueryable<T> Search<T>(
        IQueryable<T> source,
        string text,
        string[] properties
    )
    {
        try
        {
            /// Logic:
            ///     Build dynamic predicate by joining property names with OR condition.
            ///
            string predicate = string.Join(
                " OR ",
                properties.Select(property => $"{property}.Contains(@0)")
            );
            return source.Where(predicate, text);
        }
        catch
        {
            /// Logic:
            ///     Return original source if search fails due to invalid property names or query errors.
            ///
            return source;
        }
    }

    ///
    /// Summary:
    ///     Orders the queryable collection by a specified column.
    ///     Supports both ascending and descending sort orders.
    ///
    /// Parameters:
    ///     source:
    ///         The source queryable collection to order.
    ///     column:
    ///         The column name to order by.
    ///     sortOrder:
    ///         The sort order direction (ASC or DESC), defaults to DESC.
    ///
    /// Returns:
    ///     IQueryable collection ordered by the specified column and sort order, or original source if error occurs.
    ///
    public static IQueryable<T> OrderBy<T>(
        IQueryable<T> source,
        string column,
        Wks.Objects.SortOrder sortOrder = Wks.Objects.SortOrder.DESC
    )
    {
        try
        {
            /// Logic:
            ///     Determine sort direction based on SortOrder enum value.
            ///
            string typeOrder =
                sortOrder is Wks.Objects.SortOrder.DESC
                    ? nameof(Wks.Objects.SortOrder.DESC)
                    : nameof(Wks.Objects.SortOrder.ASC);

            return source.OrderBy($"{column} {typeOrder}");
        }
        catch
        {
            /// Logic:
            ///     Return original source if ordering fails due to invalid column name.
            ///
            return source;
        }
    }
}
