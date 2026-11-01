namespace Wks.UseCases;

public class CounterExpenseTransaction(DatabaseContext _databaseContext)
    : ICounterExpenseTransaction
{
    public IEnumerable<ExpenseTransactionObject.Counter> Execute(Guid profileId)
    {
        List<ExpenseTransactionObject.Counter> counters = [];

        DateTime dateTime = Moment.Utc.Now;

        IEnumerable<Expense> expense = _databaseContext
            .Expense.Include(expense => expense.ExpenseTransactions)
            .Where(expense => expense.ProfileId == profileId)
            .OrderByDescending(item => item.CreatedAt);
        IEnumerable<ExpenseTransaction> expenseTransactions = expense.SelectMany(category =>
            category.ExpenseTransactions
        );

        counters =
        [
            SubCounter(
                profileId,
                expenseTransactions,
                ExpenseTransactionCounterNames.TODAY,
                dateTime
            ),
            SubCounter(
                profileId,
                expenseTransactions,
                ExpenseTransactionCounterNames.WEEK,
                dateTime.AddDays(-Moment.WEEK)
            ),
            SubCounter(
                profileId,
                expenseTransactions,
                ExpenseTransactionCounterNames.MONTH,
                DateTime.MinValue
            ),
        ];

        return counters;
    }

    private static ExpenseTransactionObject.Counter SubCounter(
        Guid profileId,
        IEnumerable<ExpenseTransaction> expenseTransactions,
        ExpenseTransactionCounterNames expenseTransactionCounterNames,
        DateTime dateTime
    )
    {
        IEnumerable<ExpenseTransaction> _expenseTransactions = expenseTransactions.Where(
            expenseTransaction =>
                expenseTransaction.Spend && expenseTransaction.Expense.ProfileId == profileId
        );

        if (dateTime == DateTime.MinValue)
            return new(
                _expenseTransactions.Take(10),
                _expenseTransactions.Sum(item => item.Amount),
                expenseTransactionCounterNames
            );

        expenseTransactions = expenseTransactions
            .Where(expenseTransaction => expenseTransaction.Spend)
            .Where(expenseTransaction =>
                expenseTransaction.DateTime >= dateTime
                || DateOnly.FromDateTime(expenseTransaction.DateTime)
                    == DateOnly.FromDateTime(dateTime)
            )
            .OrderByDescending(expenseTransaction => expenseTransaction.Amount);

        return new(
            expenseTransactions.Take(10),
            expenseTransactions.Sum(item => item.Amount),
            expenseTransactionCounterNames
        );
    }
}
