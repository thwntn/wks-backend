namespace Wks.Objects;

///
/// Summary:
///     Defines the sort order for pagination and sorting operations.
///
public enum SortOrder
{
    ///
    /// Summary:
    ///     Ascending sort order.
    ///
    ASC,

    ///
    /// Summary:
    ///     Descending sort order.
    ///
    DESC,
}

///
/// Summary:
///     Provides static methods for creating paginated results from IQueryable data sources.
///
public class Pagination
{
    private const int NONE = 0;

    private const int DEFAULT = 1;

    ///
    /// Summary:
    ///     Creates a paginated result from an IQueryable data source.
    ///
    /// Parameters:
    ///     data:
    ///         The IQueryable data source to paginate.
    ///
    ///     queryOptions:
    ///         The query options containing search, sort, and pagination parameters.
    ///
    /// Returns:
    ///     A Pagination<TResult> object containing the paginated data and metadata.
    ///
    public static async Task<Paged<TResult>> CreateAsync<TResult>(
        IQueryable<TResult> data,
        QueryOptions queryOptions
    )
    {
        /// Logic:
        ///     Validate data source and return default if invalid.
        ///     Apply search filters if search columns and text are provided.
        ///     Apply sorting if sort column is specified.
        ///     Count total records.
        ///     Apply pagination if limit and page are specified.
        ///     Return paginated result with data and metadata.
        ///
        IQueryable<TResult> cache;
        if (data.Justifiable() is false)
            return default;

        if (
            string.IsNullOrEmpty(queryOptions.SearchText) is false
            && string.IsNullOrEmpty(queryOptions.SearchColumns) is false
        )
            cache = EFCoreExtension.Search(
                data,
                queryOptions.SearchText,
                queryOptions.SearchColumns.Split(QueryOptions.PREFIX_SPLIT)
            );
        else
            cache = data;

        if (
            queryOptions.SortColumn.Justifiable()
            && queryOptions.SortOrder.Justifiable()
            && string.IsNullOrEmpty(queryOptions.SortColumn) is false
        )
            cache = EFCoreExtension.OrderBy(
                cache.AsQueryable(),
                queryOptions.SortColumn,
                queryOptions.SortOrder
            );

        int total = await cache.CountAsync();

        if (queryOptions.Limit is not NONE && queryOptions.Page is not NONE)
        {
            cache = EnsureOrdered(cache);
            cache = cache
                .Skip((queryOptions.Page - DEFAULT) * queryOptions.Limit)
                .Take(queryOptions.Limit);
        }

        return new(
            await EntityFrameworkQueryableExtensions.ToListAsync(cache),
            total,
            queryOptions
        );
    }

    ///
    /// Summary:
    ///     Ensures row-limited queries have a stable order before pagination.
    /// Logic:
    ///     Walks the source chain, ignoring ordering inside navigation subqueries.
    ///     Treats Distinct and GroupBy as ordering boundaries.
    ///     Preserves caller ordering and adds Id as a unique tie breaker.
    ///     Otherwise sorts by CreatedAt descending when present, then Id.
    /// Parameters:
    ///     source:
    ///         The filtered query before Skip and Take are applied.
    /// Returns:
    ///     A query with deterministic ordering for a fixed data set.
    /// Exceptions:
    ///     T:System.InvalidOperationException:
    ///         The paginated result does not expose an Id property.
    ///
    private static IQueryable<TResult> EnsureOrdered<TResult>(IQueryable<TResult> source)
    {
        bool ordered = false;
        Expression expression = source.Expression;
        while (expression is MethodCallExpression call && call.Arguments.Count > NONE)
        {
            if (call.Method.DeclaringType == typeof(Queryable))
            {
                if (call.Method.Name is nameof(Queryable.Distinct) or nameof(Queryable.GroupBy))
                    break;

                if (call.Method.Name is nameof(Queryable.OrderBy) or nameof(Queryable.OrderByDescending))
                {
                    ordered = true;
                    break;
                }
            }

            expression = call.Arguments[NONE];
        }

        if (typeof(TResult).GetProperty(nameof(Entity.Id)).Justifiable() is false)
            throw new InvalidOperationException("Paginated results must expose an Id property for stable ordering.");

        if (ordered)
            return source.Provider.CreateQuery<TResult>(AppendIdentityOrder(source.Expression));

        string ordering = typeof(TResult).GetProperty(nameof(Entity.CreatedAt)).Justifiable() is false
            ? nameof(Entity.Id)
            : $"{nameof(Entity.CreatedAt)} DESC, {nameof(Entity.Id)}";
        return DynamicQueryableExtensions.OrderBy(source, ordering);
    }


    ///
    /// Summary:
    ///     Adds a unique tie breaker at the caller's ordering expression.
    /// Logic:
    ///     Rebuilds only the source chain so filters after OrderBy remain valid.
    ///     Appends ThenBy to the outermost ordered node, preserving all sort keys.
    /// Parameters:
    ///     expression:
    ///         A source chain already verified to contain effective ordering.
    /// Returns:
    ///     The rebuilt expression with Id appended to the existing ordering.
    ///
    private static Expression AppendIdentityOrder(Expression expression)
    {
        MethodCallExpression call = (MethodCallExpression)expression;
        if (call.Method.DeclaringType == typeof(Queryable)
            && call.Method.Name is nameof(Queryable.OrderBy) or nameof(Queryable.OrderByDescending)
                or nameof(Queryable.ThenBy) or nameof(Queryable.ThenByDescending))
        {
            Type elementType = call.Method.GetGenericArguments()[NONE];
            ParameterExpression item = Expression.Parameter(elementType, "item");
            MemberExpression identity = Expression.Property(item, nameof(Entity.Id));
            return Expression.Call(typeof(Queryable), nameof(Queryable.ThenBy),
                [elementType, identity.Type], call,
                Expression.Quote(Expression.Lambda(identity, item)));
        }

        Expression[] arguments = call.Arguments.ToArray();
        arguments[NONE] = AppendIdentityOrder(arguments[NONE]);
        return call.Update(call.Object, arguments);
    }

}

///
/// Summary:
///     Represents a paginated result containing data and pagination metadata.
///
public class Paged<TResult>
{
    private const int NONE = 0;

    public Paged(
        IEnumerable<TResult> data,
        int total,
        QueryOptions queryOptions
    )
    {
        /// Logic:
        ///     Handle exception cases when data is unavailable.
        ///     Initialize Cache with empty array if data is invalid, otherwise use provided data.
        ///
        if (data.Justifiable() is false)
            Cache = [];
        else
            Cache = data;

        /// Logic:
        ///     Initialize pagination metadata.
        ///     Create default page with zeros if query options are invalid.
        ///     Otherwise create page with total, limit, and current page values.
        ///
        if (queryOptions.Justifiable() is false)
            Page = new(NONE, NONE, NONE);
        else
            Page = new(total, queryOptions.Limit, queryOptions.Page);
    }

    private IEnumerable<TResult> Cache { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the list of data items for the current page.
    ///
    /// Returns:
    ///     A new list from the cache to prevent external modifications.
    ///
    public List<TResult> Data
    {
        get => [.. Cache];
        set => Cache = value;
    }

    ///
    /// Summary:
    ///     Gets or sets the pagination metadata.
    ///
    /// Returns:
    ///     The pagination metadata including total pages, current page, and limit.
    ///
    public Page Page { get; set; }
}

///
/// Summary:
///     Represents pagination metadata including total pages, current page, and page limit.
///
public class Page
{
    private const int NONE = 0;

    private const int DEFAULT = 1;

    ///
    /// Summary:
    ///     Initializes a new instance of the Page class.
    ///
    /// Parameters:
    ///     quantity:
    ///         The total number of records in the data source.
    ///
    ///     limit:
    ///         The maximum number of records per page.
    ///
    ///     current:
    ///         The current page number.
    ///
    public Page(int quantity, int limit, int current)
    {
        /// Logic:
        ///     Return early if any pagination parameter is invalid.
        ///     Calculate total pages and set pagination metadata.
        ///
        if (quantity == NONE || limit == NONE || current == NONE)
            return;

        Total = Math.Max((int)Math.Ceiling(quantity / (double)limit), DEFAULT);
        Records = quantity;
        Current = current;
        Limit = limit;
    }

    ///
    /// Summary:
    ///     Gets or sets the total number of pages available.
    ///
    /// Returns:
    ///     The total number of pages available.
    ///
    [NotMapped]
    public int Total { get; set; } = NONE;

    ///
    /// Summary:
    ///     Gets or sets the maximum number of records per page.
    ///
    /// Returns:
    ///     The maximum number of records per page.
    ///
    [NotMapped]
    public int Limit { get; set; } = NONE;

    ///
    /// Summary:
    ///     Gets or sets the current page number.
    ///
    /// Returns:
    ///     The current page number.
    ///
    [NotMapped]
    public int Current { get; set; } = NONE;

    ///
    /// Summary:
    ///     Gets or sets the total number of records.
    ///
    /// Returns:
    ///     The total number of records in the data source.
    ///
    public int Records { get; set; }
}

///
/// Summary:
///     Represents pagination metadata for API responses.
///
public class PageResponse
{
    ///
    /// Summary:
    ///     Gets or sets the total number of pages available.
    ///
    /// Returns:
    ///     The total number of pages available.
    ///
    [NotMapped]
    public int Total { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the maximum number of records per page.
    ///
    /// Returns:
    ///     The maximum number of records per page.
    ///
    [NotMapped]
    public int Limit { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the current page number.
    ///
    /// Returns:
    ///     The current page number.
    ///
    [NotMapped]
    public int Current { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the total number of records in the data source.
    ///
    /// Returns:
    ///     The total number of records in the data source.
    ///
    [NotMapped]
    public int Records { get; set; }
}

///
/// Summary:
///     Represents a paginated response containing data and pagination metadata.
///
public class PaginationResponse<TResponse>
{
    ///
    /// Summary:
    ///     Gets or sets the list of data items for the current page.
    ///
    /// Returns:
    ///     The list of data items for the current page.
    ///
    [NotMapped]
    public List<TResponse> Data { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the pagination metadata for the response.
    ///
    /// Returns:
    ///     The pagination metadata for the response.
    ///
    [NotMapped]
    public PageResponse Page { get; set; }
}
