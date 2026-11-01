namespace Wks.Objects;

public class HomeFinancialObject
{
    public class Counter(decimal income, decimal spending, decimal transaction)
    {
        public decimal Income { get; set; } = income;
        public decimal Spending { get; set; } = spending;
        public decimal Transaction { get; set; } = transaction;
    }

    public class MonthlySpending(string month, decimal amount)
    {
        public string Month { get; set; } = month;
        public decimal Amount { get; set; } = amount;
    }

    public class SpendingByExpense(string name, decimal total)
    {
        public string Name { get; set; } = name;
        public decimal Total { get; set; } = total;
    }

    public class RecentTransaction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool Spend { get; set; }
        public DateTime DateTime { get; set; }
        public string ExpenseName { get; set; }
    }
}
