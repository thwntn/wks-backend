namespace Wks.UseCases;

//
// Summary:
//      Provides expense analytics functionality for dashboard timeline counters.
//      Analyzes expense categories and their transactions to generate spending summaries.
//
public class GetExpenseAnalyst(ITimelineRepository _timelineRepository)
    : IGetExpenseAnalyst
{
    //
    // Summary:
    //      Repository for accessing expense category data and related entities.
    //
    // Returns:
    //      Instance of ITimelineRepository for data access operations.
    //

    private readonly int LIST_LIMIT = 8;

    //
    // Summary:
    //      Executes expense analytics calculation for a specific profile.
    //      Retrieves expense categories with their transactions and calculates spending totals.
    //
    // Parameters:
    //      profileId:
    //          The unique identifier of the profile to analyze expenses for.
    //
    // Returns:
    //      A list of TimelineCounter objects containing expense category summaries.
    //
    // Exceptions:
    //      T:System.Exception:
    //      Thrown when database access fails or data processing encounters errors.
    //
    public async Task<List<ReportObject.TimelineCounter>> ExecuteAsync(
        Guid profileId
    )
    {
        // Logic:
        //      Query expense categories with related expenses and transactions for the specified profile
        //      Include navigation properties to access related data efficiently
        //      Filter results by profileId to ensure data isolation
        IQueryable<Timeline> timelines = _timelineRepository
            .Include(timeline => timeline.Expenses)
                .ThenInclude(timeline => timeline.ExpenseTransactions)
            .Where(timeline => timeline.ProfileId == profileId)
            .OrderByDescending(timeline => timeline.CreatedAt)
            .Take(LIST_LIMIT);

        // Logic:
        //      Initialize empty list to store calculated timeline counters
        //      Prepare container for processed analytics data
        List<ReportObject.TimelineCounter> timelineCounters = [];

        // Logic:
        //      Process each expense category to calculate spending analytics
        //      Extract expense transactions and filter for spending transactions only
        //      Calculate total spending amount by summing transaction amounts
        //      Count total number of transactions for each category
        //      Create TimelineCounter object with category name, total amount, and transaction count
        foreach (
            var item in await EntityFrameworkQueryableExtensions.ToListAsync(
                timelines
            )
        )
            timelineCounters.Add(
                new(
                    item.Name,
                    item.Description,
                    item.Expenses.SelectMany(expense =>
                            expense.ExpenseTransactions
                        )
                        .Where(expenseTransaction => expenseTransaction.Spend)
                        .Sum(expenseTransaction => expenseTransaction.Amount),
                    item.Expenses.Sum(expense =>
                        expense.ExpenseTransactions.Count
                    ),
                    item.FromDate,
                    item.ToDate
                )
            );

        // Logic:
        //      Return the complete list of calculated timeline counters
        //      Provide analytics data for dashboard visualization
        return timelineCounters;
    }
}
