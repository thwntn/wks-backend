namespace Wks.UseCases;

public class ExpenditureExpenseTransaction(DatabaseContext _databaseContext)
    : IExpenditureExpenseTransaction
{
    public async Task<IEnumerable<DashboardObject.Value>> ExecuteAsync(
        Guid profileId,
        Guid categoryId
    )
    {
        List<DashboardObject.Value> values = (
            await _databaseContext
                .ExpenseTransaction.Where(expenseTransaction =>
                    expenseTransaction.ExpenseId == categoryId
                    && expenseTransaction.Expense.ProfileId == profileId
                    && expenseTransaction.IsLocked == false
                )
                .OrderByDescending(expenseTransaction =>
                    expenseTransaction.DateTime
                )
                .GroupBy(expenseTransaction => expenseTransaction.DateTime.Date)
                .ToListAsync()
        )
            .Select(expenseTransactionGroup => new DashboardObject.Value(
                expenseTransactionGroup.Key.ToString(),
                expenseTransactionGroup.Key,
                expenseTransactionGroup.Sum(expenseTransaction =>
                    expenseTransaction.Amount
                )
            ))
            .ToList();

        decimal maxValue = 0;
        if (values.Count is not 0)
            maxValue = values.Max(value => value.quantity);

        foreach (var value in values)
            if (maxValue is not 0)
                value.quantity = Math.Round(value.quantity / maxValue * 100);
            else
                value.quantity = maxValue;

        ///- Return expenditure calculated
        return values;
    }
}
