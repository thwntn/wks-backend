namespace Wks.Objects;

public class ExpenseObject
{
    public class Banner(decimal used, decimal totalCost, decimal usable, decimal budget)
    {
        public decimal Used { get; set; } = used;
        public decimal TotalCost { get; set; } = totalCost;
        public decimal Usable { get; set; } = usable;
        public decimal Budget { get; set; } = budget;
    }

    public class Counter(decimal inflow, decimal outflow, decimal balance, int quantity)
    {
        public decimal Inflow { get; set; } = inflow;
        public decimal Outflow { get; set; } = outflow;
        public decimal Balance { get; set; } = balance;
        public int Quantity { get; set; } = quantity;
    }
}
