namespace Wks.Databases;

///
/// Summary:
///     Represents a financial expense category within a timeline, tracking budget and transactions.
///
public class Expense(string name, string description) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the expense display name.
    ///
    /// Returns:
    ///     The human-readable name for identifying this expense category.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets whether this expense is pinned for quick access.
    ///
    /// Returns:
    ///     True if the expense is pinned; false otherwise.
    ///
    [Required]
    public bool IsPinned { get; set; } = false;

    ///
    /// Summary:
    ///     Gets or sets the expense description.
    ///
    /// Returns:
    ///     A brief description of this expense category.
    ///
    [Required]
    public string Description { get; set; } = description;

    ///
    /// Summary:
    ///     Gets or sets the planned budget amount for this expense.
    ///
    /// Returns:
    ///     The allocated budget in the smallest currency unit.
    ///
    [Required]
    public long Budget { get; set; } = 0;

    ///
    /// Summary:
    ///     Gets or sets the expense date.
    ///
    /// Returns:
    ///     The date associated with this expense category.
    ///
    [Required]
    public DateTime DateTime { get; set; }

    ///
    /// Summary:
    ///     Gets the computed total cost across all transactions.
    ///
    /// Returns:
    ///     The total cost summed from all expense transactions (not persisted).
    ///
    [NotMapped]
    public decimal TotalCost { get; set; }

    ///
    /// Summary:
    ///     Gets the computed total inflow amount.
    ///
    /// Returns:
    ///     The total income amount aggregated from transactions (not persisted).
    ///
    [NotMapped]
    public decimal Inflow { get; set; }

    ///
    /// Summary:
    ///     Gets the computed total outflow amount.
    ///
    /// Returns:
    ///     The total spending amount aggregated from transactions (not persisted).
    ///
    [NotMapped]
    public decimal Outflow { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the timeline identifier this expense belongs to.
    ///
    /// Returns:
    ///     The unique identifier of the parent timeline.
    ///
    [Required]
    public Guid TimelineId { get; set; }

    [ForeignKey(nameof(TimelineId))]
    public Timeline Timeline { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile that owns this expense.
    ///
    [Required]
    public Guid ProfileId { get; set; }

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }

    #region Reference
    ///
    /// Summary:
    ///     Gets or sets the collection of transactions for this expense.
    ///
    /// Returns:
    ///     The expense transaction records associated with this expense category.
    ///
    public ICollection<ExpenseTransaction> ExpenseTransactions { get; set; }
    #endregion
}
