namespace Wks.Databases;

///
/// Summary:
///     Represents a financial timeline grouping expenses within a date range for budget tracking.
///
public class Timeline(string name, string description) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the timeline display name.
    ///
    /// Returns:
    ///     The human-readable name for identifying this timeline.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the timeline description.
    ///
    /// Returns:
    ///     A brief description of this timeline's purpose or coverage period.
    ///
    [Required]
    public string Description { get; set; } = description;

    ///
    /// Summary:
    ///     Gets or sets the timeline start date.
    ///
    /// Returns:
    ///     The UTC date and time when this timeline period begins.
    ///
    [Required]
    public DateTime FromDate { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the timeline end date.
    ///
    /// Returns:
    ///     The UTC date and time when this timeline period ends.
    ///
    [Required]
    public DateTime ToDate { get; set; }

    ///
    /// Summary:
    ///     Gets or sets whether this timeline is pinned for quick access.
    ///
    /// Returns:
    ///     True if the timeline is pinned; false otherwise.
    ///
    [Required]
    public bool IsPinned { get; set; } = false;

    ///
    /// Summary:
    ///     Gets the computed total income across all expenses.
    ///
    /// Returns:
    ///     The total inflow amount aggregated from expense transactions (not persisted).
    ///
    [NotMapped]
    public decimal Income { get; set; }

    ///
    /// Summary:
    ///     Gets the computed total spending across all expenses.
    ///
    /// Returns:
    ///     The total outflow amount aggregated from expense transactions (not persisted).
    ///
    [NotMapped]
    public decimal Outcome { get; set; }

    ///
    /// Summary:
    ///     Gets the computed total transaction count across all expenses.
    ///
    /// Returns:
    ///     The number of transactions in this timeline (not persisted).
    ///
    [NotMapped]
    public int QuantityTransaction { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile that owns this timeline.
    ///
    [Required]
    public Guid ProfileId { get; set; }

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }

    #region Reference
    ///
    /// Summary:
    ///     Gets or sets the collection of expenses in this timeline.
    ///
    /// Returns:
    ///     The expense records grouped under this timeline.
    ///
    public ICollection<Expense> Expenses { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the collection of hashtag associations.
    ///
    /// Returns:
    ///     The hashtags linked to this timeline for categorization.
    ///
    public ICollection<TimelineTag> TimelineTags { get; set; }
    #endregion
}
