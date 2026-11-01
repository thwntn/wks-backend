namespace Wks.Objects;

public class TimelineObject
{
    public class Counter(
        decimal income,
        decimal outcome,
        int quantityCategory,
        int quantityTransaction
    )
    {
        public decimal InCome { get; set; } = income;
        public decimal OutCome { get; set; } = outcome;
        public int QuantityCategory { get; set; } = quantityCategory;
        public int QuantityTransaction { get; set; } = quantityTransaction;
    }

    public class Chart(string name, decimal count)
    {
        public string Name { get; set; } = name;
        public decimal Count { get; set; } = count;
    }

    public class RecentSummary(decimal income, decimal outcome)
    {
        public decimal InCome { get; set; } = income;
        public decimal OutCome { get; set; } = outcome;
    }

    public class Recent(
        RecentSummary summary,
        Paged<ExpenseTransaction> transactions
    )
    {
        public RecentSummary Summary { get; set; } = summary;
        public Paged<ExpenseTransaction> Transactions { get; set; } =
            transactions;
    }

    public class Period(string name, decimal income, decimal outcome, decimal budget)
    {
        public string Name { get; set; } = name;
        public decimal InCome { get; set; } = income;
        public decimal OutCome { get; set; } = outcome;
        public decimal Budget { get; set; } = budget;
    }

    public class Cashflow(
        string name,
        DateTime fromDate,
        DateTime toDate,
        decimal income,
        decimal outcome
    )
    {
        public string Name { get; set; } = name;
        public DateTime FromDate { get; set; } = fromDate;
        public DateTime ToDate { get; set; } = toDate;
        public decimal InCome { get; set; } = income;
        public decimal OutCome { get; set; } = outcome;
    }
}
