namespace Wks.Databases;

using Wks.Interfaces;

///
/// Summary:
///     Represents a descriptive line item entry within a cash transaction record.
///
public class CashDescription(string name, string quantity) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the description display name.
    ///
    /// Returns:
    ///     The human-readable label for this cash description entry.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the quantity string for this description entry.
    ///
    /// Returns:
    ///     The quantity value associated with this cash description.
    ///
    [Required]
    public string Quantity { get; set; } = quantity;

    ///
    /// Summary:
    ///     Gets or sets the cash transaction type.
    ///
    /// Returns:
    ///     The type enum indicating the cash operation direction.
    ///
    [Required]
    public CashNames CashNames { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the parent cash record identifier.
    ///
    /// Returns:
    ///     The unique identifier of the cash record this description belongs to.
    ///
    [Required]
    public Guid CashId { get; set; }

    [ForeignKey(nameof(CashId))]
    public Cash Cash { get; set; }
}
