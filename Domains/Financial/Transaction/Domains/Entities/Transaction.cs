namespace Wks.Databases;

///
/// Summary:
///     Represents a single financial transaction within an expense category.
///
public class ExpenseTransaction(
    string name,
    string description,
    decimal amount,
    DateTime dateTime,
    Guid expenseId,
    bool spend = true,
    bool isLocked = false
) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the transaction display name.
    ///
    /// Returns:
    ///     The human-readable name describing this transaction.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the transaction description.
    ///
    /// Returns:
    ///     A brief description of what this transaction represents.
    ///
    [Required]
    public string Description { get; set; } = description;

    ///
    /// Summary:
    ///     Gets or sets the transaction date.
    ///
    /// Returns:
    ///     The UTC date and time when this transaction occurred.
    ///
    [Required]
    public DateTime DateTime { get; set; } = dateTime;

    ///
    /// Summary:
    ///     Gets or sets whether this transaction is a spending (outflow) transaction.
    ///
    /// Returns:
    ///     True if this transaction is a spend (outflow); false if it is income (inflow).
    ///
    [Required]
    public bool Spend { get; set; } = spend;

    ///
    /// Summary:
    ///     Gets or sets whether this transaction is locked from editing.
    ///
    /// Returns:
    ///     True if the transaction is locked and cannot be modified; false if editable.
    ///
    [Required]
    public bool IsLocked { get; set; } = isLocked;

    ///
    /// Summary:
    ///     Gets or sets the transaction amount.
    ///
    /// Returns:
    ///     The monetary amount of this transaction.
    ///
    [Required]
    public decimal Amount { get; set; } = amount;

    ///
    /// Summary:
    ///     Gets or sets the parent expense identifier.
    ///
    /// Returns:
    ///     The unique identifier of the expense category this transaction belongs to.
    ///
    [Required]
    public Guid ExpenseId { get; set; } = expenseId;

    [ForeignKey(nameof(ExpenseId))]
    public Expense Expense { get; set; }
}
