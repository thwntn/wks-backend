namespace Wks.Objects;

public class ReportObject
{
    public class TimelineCounter(
        string name,
        string description,
        decimal amount,
        int quantityTransaction,
        DateTime fromDate,
        DateTime toDate
    )
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public decimal Amount { get; set; } = amount;
        public int QuantityTransaction { get; set; } = quantityTransaction;
        public DateTime FromDate { get; set; } = fromDate;
        public DateTime ToDate { get; set; } = toDate;
    }

    public class Statement(
        Summary summary,
        IEnumerable<Category> categories,
        IEnumerable<Month> months,
        IEnumerable<Payment> largest
    )
    {
        public Summary Summary { get; set; } = summary;
        public IEnumerable<Category> Categories { get; set; } = categories;
        public IEnumerable<Month> Months { get; set; } = months;
        public IEnumerable<Payment> Largest { get; set; } = largest;
    }

    public class Summary(decimal income, decimal outcome, decimal profit, int transactions)
    {
        public decimal Income { get; set; } = income;
        public decimal Outcome { get; set; } = outcome;
        public decimal Profit { get; set; } = profit;
        public int Transactions { get; set; } = transactions;
    }

    public class Category(string name, decimal amount, int count)
    {
        public string Name { get; set; } = name;
        public decimal Amount { get; set; } = amount;
        public int Count { get; set; } = count;
    }

    public class Month(string label, decimal income, decimal outcome)
    {
        public string Label { get; set; } = label;
        public decimal Income { get; set; } = income;
        public decimal Outcome { get; set; } = outcome;
    }

    public class Payment(
        string name,
        string description,
        decimal amount,
        DateTime dateTime,
        string expense
    )
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public decimal Amount { get; set; } = amount;
        public DateTime DateTime { get; set; } = dateTime;
        public string Expense { get; set; } = expense;
    }
}

public class DashboardObject
{
    public class Overview(
        Expenditure<ExpenseTransaction> timelineTransaction,
        Expenditure<CashDescription> cash
    )
    {
        public Expenditure<ExpenseTransaction> timelineTransaction = timelineTransaction;
        public Expenditure<CashDescription> cash = cash;
    }

    public class Expenditure<T>
    {
        public Counter counter = new(0, 0, 0);
        public Count<T> chart = new();
        public IEnumerable<T> recent = [];
    }

    public class Counter(long totalCost, long totalItem, long thisMonth)
    {
        public long totalItem = totalItem;
        public long totalCost = totalCost;
        public long thisMonth = thisMonth;
    }

    public class Count<T>
    {
        public IEnumerable<Value> thisMonth;
        public IEnumerable<Value> lastMonth;
    }

    public class Value(string name, DateTime dateTime, decimal quantity)
    {
        public string name = name;
        public DateTime dateTime = dateTime;
        public decimal quantity = quantity;
    }
}
