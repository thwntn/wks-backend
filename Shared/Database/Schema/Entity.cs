namespace Wks.Databases;

///
/// Summary:
///     Represents the base entity for all domain models in the system.
///     Provides common auditing properties including identity, creation,
///     update, and soft delete tracking information.
///     All entities should inherit from this class to ensure consistency
///     across persistence and lifecycle management.
///
public class Entity
{
    ///
    /// Summary:
    ///     Gets the unique identifier of the entity.
    ///
    /// Returns:
    ///     A globally unique identifier (GUID) representing the entity ID.
    ///
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.CreateVersion7();

    ///
    /// Summary:
    ///     Gets or sets the timestamp when the entity was created.
    ///
    /// Returns:
    ///     A UTC DateTime representing the creation time of the entity.
    ///
    [Required]
    public DateTime CreatedAt { get; set; } = Moment.Utc.Now;

    ///
    /// Summary:
    ///     Gets or sets the timestamp when the entity was last updated.
    ///
    /// Returns:
    ///     A nullable UTC DateTime representing the last update time,
    ///     or null if the entity has not been updated.
    ///
    [AllowNull]
    public DateTime? UpdatedAt { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the timestamp when the entity was soft deleted.
    ///
    /// Returns:
    ///     A nullable UTC DateTime representing the deletion time,
    ///     or null if the entity is active.
    ///
    [AllowNull]
    public DateTime? DeletedAt { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the identifier of the profile that performed
    ///     the soft delete operation.
    ///
    /// Returns:
    ///     A nullable GUID representing the profile ID responsible
    ///     for deleting the entity, or null if not deleted.
    ///
    [AllowNull]
    public Guid? DeletedBy { get; set; }

    ///
    /// Summary:
    ///     Restores a soft-deleted entity to active state.
    ///
    /// Logic:
    ///     Sets DeletedAt to null.
    ///     Sets DeletedBy to null.
    ///
    /// Exceptions:
    ///     None.
    ///
    public void Restore()
    {
        DeletedAt = null;
        DeletedBy = null;
    }
}
