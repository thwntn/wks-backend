namespace Wks.Objects;

public class ExpenseTransactionObject
{
    public class Counter(
        IEnumerable<ExpenseTransaction> items,
        decimal totalCost,
        ExpenseTransactionCounterNames ExpenseTransactionCounterNames
    )
    {
        public IEnumerable<ExpenseTransaction> Items { get; set; } = items;
        public decimal TotalCost { get; set; } = totalCost;
        public ExpenseTransactionCounterNames ExpenseTransactionCounterNames { get; set; } =
            ExpenseTransactionCounterNames;
    }

    public class Statistic(string name, decimal value)
    {
        public string Name { get; set; } = name;
        public decimal Value { get; set; } = value;
    }
}
